using Microsoft.VisualStudio.TestTools.UnitTesting;
using InMemoryDatabase.Controllers;
using InMemoryDatabase.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using InMemoryDatabase.Models;

namespace InMemoryDatabase.Test
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void TestCustomizeDishView()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("DefaultConnection"));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.Dishes.Add(new Dish());
            context.Dishes.Add(new Dish());
            context.SaveChanges();

            var controller = new HomeController(context);

            var result = controller.CustomizeDish(1) as ViewResult;

            Assert.IsNotNull(result);
            //Assert.AreEqual("CustomizeDish", result.ViewName);
        }
    }
}
