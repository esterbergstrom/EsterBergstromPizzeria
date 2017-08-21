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
        public static void Initialize(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser()
            {
                UserName = "jane.doe@gmail.com",
                Email = "jane.doe@gmail.com"
            };

            userManager.CreateAsync(user, "Password123!");
        }
    }
}
