using System;
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

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DishIngredient>().HasKey(x => new { x.DishID, x.IngredientID });

            builder.Entity<DishIngredient>()
                .HasOne(x => x.Dish)
                .WithMany(x => x.DishIngredients)
                .HasForeignKey(x => x.DishID);

            builder.Entity<DishIngredient>()
                .HasOne(x => x.Ingredient)
                .WithMany(x => x.DishIngredients)
                .HasForeignKey(x => x.IngredientID);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
