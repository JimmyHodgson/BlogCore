using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Common;
using BlogCore.Models.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace BlogCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IHostingEnvironment _env;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configuration variable declaration
            string emailAccount,
                emailPassword,
                emailSenderAccount,
                emailSenderName,
                emailServer,
                databasePassword;
            short emailPort;
            bool emailTLS;

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            if (_env.IsDevelopment())
            {
                emailAccount = Configuration["mail:account"];
                emailPassword = Configuration["mail:secret"];
                emailPort = Convert.ToInt16(Configuration["mail:port"]);
                emailTLS = Convert.ToBoolean(Configuration["mail:tls"]);
                emailSenderAccount = Configuration["mail:senderaccount"];
                emailSenderName = Configuration["mail:sendername"];
                emailServer = Configuration["mail:server"];
                databasePassword = Configuration["database:password"];
            }
            else
            {
                emailAccount = Environment.GetEnvironmentVariable(Constants.Env.MailAccount);
                emailPassword = Environment.GetEnvironmentVariable(Constants.Env.MailSecret);
                emailPort = Convert.ToInt16(Environment.GetEnvironmentVariable(Constants.Env.MailPort));
                emailTLS = Convert.ToBoolean(Environment.GetEnvironmentVariable(Constants.Env.MailTLS));
                emailSenderAccount = Environment.GetEnvironmentVariable(Constants.Env.MailSenderAccount);
                emailSenderName = Environment.GetEnvironmentVariable(Constants.Env.MailSenderName);
                emailServer = Environment.GetEnvironmentVariable(Constants.Env.MailServer);
                databasePassword = Environment.GetEnvironmentVariable(Constants.Env.SqlSecret);
            }

            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("default"))
            {
                Password = databasePassword
            };


            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.ConnectionString));

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(1);
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new MailKitOptions()
                {
                    Server = emailServer,
                    Port = emailPort,
                    SenderName = emailSenderName,
                    SenderEmail = emailSenderAccount,
                    Account = emailAccount,
                    Password = emailPassword,
                    Security = emailTLS
                });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"assets")),
                RequestPath = new PathString("/assets")
            });
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
