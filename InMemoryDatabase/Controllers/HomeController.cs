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
            var dishes = _context.Dishes
                .Where(x => x.Category == Enums.DishCategory.ItalianPizza)
                .Select(x => x)
                .ToList();

            var viewModel = new HomeViewModel()
            {
                Category = Enums.DishCategory.ItalianPizza,
                Dishes = dishes
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(Enums.DishCategory category)
        {
            var dishes = _context.Dishes
                .Where(x => x.Category == category)
                .Select(x => x)
                .ToList();

            var viewModel = new HomeViewModel()
            {
                Category = category,
                Dishes = dishes
            };
            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
