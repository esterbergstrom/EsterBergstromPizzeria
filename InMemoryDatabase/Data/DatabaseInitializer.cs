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

            CreateCategories(context);
            CreateDishes(context);
            CreateExtras(context);
            CreateDishExtras(context);
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

        private static void CreateCategories(ApplicationDbContext context)
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Italienska pizzor"
                },
                new Category()
                {
                    Name = "Specialpizzor"
                },
                new Category()
                {
                    Name = "Sallader"
                }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
        
        private static void CreateDishes(ApplicationDbContext context)
        {
            var dishes = new List<Dish>()
            {
                new Dish()
                {
                    Name = "Margherita",
                    Price = 65,
                    Description = "Tomatsås, ost",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png",
                    CategoryId = context.Categories.Where(x => x.Name == "Italienska pizzor").First().CategoryId
                },
                new Dish()
                {
                    Name = "Vesuvio",
                    Price = 65,
                    Description = "Skinka",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png",
                    CategoryId = context.Categories.Where(x => x.Name == "Italienska pizzor").First().CategoryId
                },
                new Dish()
                {
                    Name = "Tonno",
                    Price = 65,
                    Description = "Tonfisk",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png",
                    CategoryId = context.Categories.Where(x => x.Name == "Italienska pizzor").First().CategoryId
                },
                new Dish()
                {
                    Name = "Amalfi",
                    Price = 65,
                    Description = "Räkor",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png",
                    CategoryId = context.Categories.Where(x => x.Name == "Italienska pizzor").First().CategoryId
                },
                new Dish()
                {
                    Name = "Funghi",
                    Price = 65,
                    Description = "Champinjoner",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png",
                    CategoryId = context.Categories.Where(x => x.Name == "Italienska pizzor").First().CategoryId
                },
                new Dish()
                {
                    Name = "Calzone",
                    Price = 85,
                    Description = "Skinka, räkor, svamp",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png",
                    CategoryId = context.Categories.Where(x => x.Name == "Specialpizzor").First().CategoryId
                },
                new Dish()
                {
                    Name = "Fantasia",
                    Price = 85,
                    Description = "Skinka, salami, paprika, lök",
                    ImageURL = "https://cdn.modpizza.com/wp-content/uploads/2016/11/mod-pizza-mad-dog-e1479167997381.png",
                    CategoryId = context.Categories.Where(x => x.Name == "Specialpizzor").First().CategoryId
                },
                new Dish()
                {
                    Name = "Ost- och skinksallad",
                    Price = 80,
                    Description = "Isbergssallad, tomat, paprika, gurka, ost, skinka, ägg",
                    ImageURL = "http://graphics.frontiercoop.com/grilling/simplyorganic/imgs/topdown/top-down-pasta.png",
                    CategoryId = context.Categories.Where(x => x.Name == "Sallader").First().CategoryId
                },
                new Dish()
                {
                    Name = "Räksallad",
                    Price = 80,
                    Description = "Isbergssallad, tomat, paprika, gurka, räkor, ägg",
                    ImageURL = "http://graphics.frontiercoop.com/grilling/simplyorganic/imgs/topdown/top-down-pasta.png",
                    CategoryId = context.Categories.Where(x => x.Name == "Sallader").First().CategoryId
                }
            };

            context.Dishes.AddRange(dishes);
            context.SaveChanges();
        }

        private static void CreateExtras(ApplicationDbContext context)
        {
            var extras = new List<Extra>()
            {
                new Extra()
                {
                    Name = "Pizzasallad",
                    Price = 15
                },
                new Extra()
                {
                    Name = "Bearnaisesås",
                    Price = 15
                },
                new Extra()
                {
                    Name = "Tomat",
                    Price = 10
                },
                new Extra()
                {
                    Name = "Jalapeño",
                    Price = 10
                }
            };

            context.Extras.AddRange(extras);
            context.SaveChanges();
        }

        public static void CreateDishExtras(ApplicationDbContext context)
        {
            var dishExtras = new List<DishExtra>()
            {
                new DishExtra()
                {
                    Dish = context.Dishes.Where(x => x.Name == "Margherita").First(),
                    Extra = context.Extras.Where(x => x.Name == "Pizzasallad").First()
                },
                new DishExtra()
                {
                    Dish = context.Dishes.Where(x => x.Name == "Margherita").First(),
                    Extra = context.Extras.Where(x => x.Name == "Tomat").First()
                }
            };

            context.DishExtras.AddRange(dishExtras);
            context.SaveChanges();
        }
    }
}
