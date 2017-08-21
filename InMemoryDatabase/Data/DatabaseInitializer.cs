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
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (userManager.Users.Count() <= 0)
            {
                CreateUsers(userManager);
            }

            if (context.Dishes.Count() <= 0)
            {
                CreateDishes(context);
            }
        }

        private static void CreateUsers(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser()
            {
                UserName = "student@test.com",
                Email = "student@test.com"
            };

            userManager.CreateAsync(user, "Password123!");
        }

        private static void CreateDishes(ApplicationDbContext context)
        {
            var pizzas = new List<Dish>()
            {
                new Dish()
                {
                    Name = "Margarita",
                    Price = 65
                },
                new Dish()
                {
                    Name = "Bosola",
                    Price = 82
                },
                new Dish()
                {
                    Name = "Tigris",
                    Price = 85
                }
            };

            context.AddRange(pizzas);
            context.SaveChanges();
        }
             
    }
}
