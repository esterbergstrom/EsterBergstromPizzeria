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

            var categories = new List<string>();
            foreach (var c in _context.Categories.ToList())
            {
                categories.Add(c.Name);
            }

            ViewData["SelectedCategory"] = category;
            ViewData["Categories"] = categories.ToArray();

            return View(dishes);
        }

        [HttpPost]
        public IActionResult Index(string category)
        {
            var dishes = _context.Dishes
                .Where(x => x.Category.Name == category)
                .Select(x => x)
                .ToList();

            var categories = new List<string>();
            foreach (var c in _context.Categories.ToList())
            {
                categories.Add(c.Name);
            }

            ViewData["SelectedCategory"] = category;
            ViewData["Categories"] = categories.ToArray();

            return View(dishes);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
