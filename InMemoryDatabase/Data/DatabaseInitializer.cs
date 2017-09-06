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
            CreateUsers(userManager, roleManager);
            CreateDishes(context);
        }

        private static void CreateUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            roleManager.CreateAsync(new IdentityRole("Administrator"));

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
            var dishes = new List<Dish>()
            {
                new Dish()
                {
                    Name = "Margherita",
                    Price = 65,
                    Category = Enums.DishCategory.ItalianPizza,
                    Description = "Tomatsås, ost",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png"
                },
                new Dish()
                {
                    Name = "Vesuvio",
                    Price = 65,
                    Category = Enums.DishCategory.ItalianPizza,
                    Description = "Skinka",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png"
                },
                new Dish()
                {
                    Name = "Tonno",
                    Price = 65,
                    Category = Enums.DishCategory.ItalianPizza,
                    Description = "Tonfisk",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png"
                },
                new Dish()
                {
                    Name = "Amalfi",
                    Price = 65,
                    Category = Enums.DishCategory.ItalianPizza,
                    Description = "Räkor",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png"
                },
                new Dish()
                {
                    Name = "Funghi",
                    Price = 65,
                    Category = Enums.DishCategory.ItalianPizza,
                    Description = "Champinjoner",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png"
                },
                new Dish()
                {
                    Name = "Calzone",
                    Price = 85,
                    Category = Enums.DishCategory.SpecialPizza,
                    Description = "Skinka, räkor, svamp",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png"
                },
                new Dish()
                {
                    Name = "Fantasia",
                    Price = 85,
                    Category = Enums.DishCategory.SpecialPizza,
                    Description = "Skinka, salami, paprika, lök",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png"
                },
                new Dish()
                {
                    Name = "Ost- och skinksallad",
                    Price = 80,
                    Category = Enums.DishCategory.Salad,
                    Description = "Isbergssallad, tomat, paprika, gurka, ost, skinka, ägg",
                    ImageURL = "http://graphics.frontiercoop.com/grilling/simplyorganic/imgs/topdown/top-down-pasta.png"
                },
                new Dish()
                {
                    Name = "Räksallad",
                    Price = 80,
                    Category = Enums.DishCategory.Salad,
                    Description = "Isbergssallad, tomat, paprika, gurka, räkor, ägg",
                    ImageURL = "http://graphics.frontiercoop.com/grilling/simplyorganic/imgs/topdown/top-down-pasta.png"
                }
            };

            context.Dishes.AddRange(dishes);
            context.SaveChanges();
        }
    }
}
