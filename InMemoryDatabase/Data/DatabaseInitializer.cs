using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemoryDatabase.Models;
using Microsoft.AspNetCore.Identity;

namespace InMemoryDatabase.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            roleManager.CreateAsync(new IdentityRole("Administrator"));
            CreateUsers(userManager);

            CreateDishes(context);
        }

        private static void CreateUsers(UserManager<ApplicationUser> userManager)
        {
            var regularUser = new ApplicationUser()
            {
                UserName = "student@test.com",
                Email = "student@test.com"
            };
            var adminUser = new ApplicationUser()
            {
                UserName = "admin@test.com",
                Email = "admin@test.com"
            };

            userManager.CreateAsync(regularUser, "Password123!");
            userManager.CreateAsync(adminUser, "Password123!");

            userManager.AddToRoleAsync(adminUser, "Administrator");
        }

        private static void CreateDishes(ApplicationDbContext context)
        {
            var margarita = new Dish()
            {
                Name = "Margarita",
                Price = 65
            };
            var bosola = new Dish()
            {
                Name = "Bosola",
                Price = 82
            };
            var tigris = new Dish()
            {
                Name = "Tigris",
                Price = 85
            };

            var ingredients = new Dictionary<string, Ingredient>();
            ingredients.Add("Mozzarella", new Ingredient() { Name = "Mozzarella" });
            ingredients.Add("Parmesan", new Ingredient() { Name = "Parmesan" });
            ingredients.Add("Tomatoes", new Ingredient() { Name = "Tomatoes" });
            ingredients.Add("Ham", new Ingredient() { Name = "Ham" });
            ingredients.Add("Prawns", new Ingredient() { Name = "Prawns" });
            ingredients.Add("Onions", new Ingredient() { Name = "Onions" });

            margarita.DishIngredients = new List<DishIngredient>()
            {
                new DishIngredient()
                {
                    Dish = margarita,
                    Ingredient = ingredients["Mozzarella"]
                },
                new DishIngredient()
                {
                    Dish = margarita,
                    Ingredient = ingredients["Parmesan"]
                },
                new DishIngredient()
                {
                    Dish = margarita,
                    Ingredient = ingredients["Tomatoes"]
                }
            };

            bosola.DishIngredients = new List<DishIngredient>()
            {
                new DishIngredient()
                {
                    Dish = margarita,
                    Ingredient = ingredients["Ham"]
                },
                new DishIngredient()
                {
                    Dish = margarita,
                    Ingredient = ingredients["Prawns"]
                }
            };

            tigris.DishIngredients = new List<DishIngredient>()
            {
                new DishIngredient()
                {
                    Dish = margarita,
                    Ingredient = ingredients["Onions"]
                }
            };

            context.Dishes.AddRange(margarita, bosola, tigris);
            context.Ingredients.AddRange(ingredients.Values);
            context.SaveChanges();
        }
    }
}
