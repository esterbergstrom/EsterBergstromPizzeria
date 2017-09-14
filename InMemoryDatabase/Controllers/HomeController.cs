using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InMemoryDatabase.Models;
using InMemoryDatabase.Data;

namespace InMemoryDatabase.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var category = "Italienska pizzor";

            var dishes = _context.Dishes
                .Where(x => x.Category.Name == category)
                .Select(x => x)
                .ToList();

            ViewData["CurrentPage"] = category;
            ViewData["Categories"] = GetAllCategories();

            return View(dishes);
        }

        [HttpPost]
        public IActionResult Index(string category)
        {
            var dishes = _context.Dishes
                .Where(x => x.Category.Name == category)
                .Select(x => x)
                .ToList();

            ViewData["CurrentPage"] = category;
            ViewData["Categories"] = GetAllCategories();

            return View(dishes);
        }

        [HttpGet]
        public IActionResult CustomizeDish(int dishId)
        {
            var dish = _context.Dishes
                .Where(x => x.DishId == dishId)
                .First();

            var extras = new List<Extra>();
            var dishExtras = _context.DishExtras
                .Where(x => x.Dish == dish)
                .ToList();
            foreach (var dishExtra in dishExtras)
            {
                extras.Add(dishExtra.Extra);
            }

            var viewModel = new CustomizeDishViewModel()
            {
                Dish = dish,
                Extras = extras
            };

            ViewData["CurrentPage"] = dish.Name;
            ViewData["Categories"] = GetAllCategories();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CustomizeDish()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string[] GetAllCategories()
        {
            var categories = new List<string>();

            foreach (var c in _context.Categories.ToList())
            {
                categories.Add(c.Name);
            }

            return categories.ToArray();
        }
    }
}
