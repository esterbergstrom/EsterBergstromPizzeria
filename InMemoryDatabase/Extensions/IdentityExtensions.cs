using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Extensions
{
    public static class IdentityExtensions
    {
        public static Task<T> GetCurrentUser<T>(this UserManager<T> manager, HttpContext httpContext) where T: class
        {
            return manager.GetUserAsync(httpContext.User);
        }
    }
}
