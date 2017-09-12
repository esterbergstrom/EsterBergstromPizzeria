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
            var categoryName = "Italienska pizzor";

            var dishes = _context.Dishes
                .Where(x => x.Category.Name == categoryName)
                .ToList();

            var categories = _context.Categories.ToList();
            var categoryNames = new List<string>();
            foreach (var category in categories)
            {
                categoryNames.Add(category.Name);
            }

            ViewData["SelectedCategory"] = categoryName;
            ViewData["Categories"] = categoryNames.ToArray();

            return View(dishes);
        }

        [HttpPost]
        public IActionResult Index(string categoryName)
        {
            var dishes = _context.Dishes
                .Where(x => x.Category.Name == categoryName)
                .ToList();

            var categories = _context.Categories.ToList();
            var categoryNames = new List<string>();
            foreach (var category in categories)
            {
                categoryNames.Add(category.Name);
            }

            ViewData["SelectedCategory"] = categoryName;
            ViewData["Categories"] = categoryNames.ToArray();

            return View(dishes);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
