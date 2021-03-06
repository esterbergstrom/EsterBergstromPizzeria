﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InMemoryDatabase.Models;

namespace InMemoryDatabase.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<DishExtra> DishExtras { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<DishExtra>()
                .HasKey(de => new { de.DishId, de.ExtraId });

            builder.Entity<DishExtra>()
                .HasOne(de => de.Dish)
                .WithMany(d => d.DishExtras)
                .HasForeignKey(de => de.DishId);

            builder.Entity<DishExtra>()
                .HasOne(de => de.Extra)
                .WithMany(e => e.DishExtras)
                .HasForeignKey(de => de.ExtraId);
        }
    }
}
