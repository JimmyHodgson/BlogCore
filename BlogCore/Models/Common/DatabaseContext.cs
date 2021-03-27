﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogCore.Models.Catalogues;
using BlogCore.Common;

namespace BlogCore.Models.Common
{
    public partial class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }
        public DbSet<AchievementModel> Achievements { get; set; }
        public DbSet<EducationModel> Education { get; set; }
        public DbSet<JobModel> Jobs { get; set; }
        public DbSet<SkillModel> Skills { get; set; }
        public DbSet<LinkModel> Links { get; set; }
        public DbSet<MediaLinkModel> MediaLinks { get; set; }

    }
}
