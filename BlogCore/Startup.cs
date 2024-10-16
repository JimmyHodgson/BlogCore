﻿using System;
using System.IO;
using System.Linq;
using Amazon.S3;
using BlogCore.Common;
using BlogCore.Common.ReCaptcha;
using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace BlogCore
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        public Startup(IConfiguration configuration, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configuration variable declaration
            string mailAccount,
                mailPassword,
                mailSenderAccount,
                mailSenderName,
                mailServer,
                databaseSecret,
                databaseServer,
                databaseName,
                databaseUser;
            short mailPort;
            bool mailTLS;

            mailAccount = Configuration[Constants.Configuration.MailAccount];
            mailPassword = Configuration[Constants.Configuration.MailSecret];
            mailPort = Convert.ToInt16(Configuration[Constants.Configuration.MailPort]);
            mailTLS = Convert.ToBoolean(Configuration[Constants.Configuration.MailTLS]);
            mailSenderAccount = Configuration[Constants.Configuration.MailSenderAccount];
            mailSenderName = Configuration[Constants.Configuration.MailSenderName];
            mailServer = Configuration[Constants.Configuration.MailServer];
            databaseSecret = Configuration[Constants.Configuration.DatabaseSecret];
            databaseServer = Configuration[Constants.Configuration.DatabaseServer];
            databaseName = Configuration[Constants.Configuration.DatabaseName];
            databaseUser = Configuration[Constants.Configuration.DatabaseUser];


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var builder = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString("default")
                .Replace($"<{Constants.Configuration.DatabaseServer}>", databaseServer)
                .Replace($"<{Constants.Configuration.DatabaseName}>",databaseName)
                .Replace($"<{Constants.Configuration.DatabaseUser}>",databaseUser)
                )
            {
                Password = databaseSecret
            };

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.ConnectionString));

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 6;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.User.RequireUniqueEmail = true;
                config.SignIn.RequireConfirmedEmail = true;
                config.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            })
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation");

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));
            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(3));

            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddOData();

            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new MailKitOptions()
                {
                    Server = mailServer,
                    Port = mailPort,
                    SenderName = mailSenderName,
                    SenderEmail = mailSenderAccount,
                    Account = mailAccount,
                    Password = mailPassword,
                    Security = mailTLS
                });
            });

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();

            services.AddSingleton(Configuration);

            services.AddOptions<CaptchaSettings>().Bind(Configuration.GetSection("Captcha"));
            services.AddTransient<CaptchaVerificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Apply the status code pages conditionally
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 && !context.Request.Path.Value.Contains("/api/"))
                {
                    context.Request.Path = "/Home/ContentNotFound";
                    await next();
                }
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"assets")),
                RequestPath = new PathString("/assets")
            });
            app.UseAuthentication();
            app.UseCookiePolicy();

            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<JobModel>("Job");
            builder.EntitySet<SkillModel>("Skill");
            builder.EntitySet<EducationModel>("Education");
            builder.EntitySet<MediaLinkModel>("MediaLink");
            builder.EntitySet<MediaGroupModel>("MediaGroup");
            builder.EntitySet<AchievementModel>("Achievement");

            app.UseMvc(routes =>
            {
                routes.Select().Expand().Filter().OrderBy().MaxTop(null).Count();
                routes.MapRoute(
                    name:"default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapODataServiceRoute("ODataRoute", "api", builder.GetEdmModel());
            });

        }
    }
}
