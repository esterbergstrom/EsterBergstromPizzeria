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
            var categoryId = _context.Categories
                .Where(x => x.Name == "Italienska pizzor")
                .First()
                .CategoryId;

            var dishes = _context.Dishes
                .Where(x => x.Category.CategoryId == categoryId)
                .Select(x => x)
                .ToList();

            var availableCategories = _context.Categories.ToList();

            var selectedCategory = availableCategories.Find(x => x.CategoryId == categoryId);
            availableCategories.Remove(selectedCategory);

            var viewModel = new HomeViewModel()
            {
                Dishes = dishes,
                SelectedCategory = selectedCategory,
                AvailableCategories = availableCategories
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(int categoryId)
        {
            var dishes = _context.Dishes
                .Where(x => x.Category.CategoryId == categoryId)
                .Select(x => x)
                .ToList();

            var availableCategories = _context.Categories.ToList();

            var selectedCategory = availableCategories.Find(x => x.CategoryId == categoryId);
            availableCategories.Remove(selectedCategory);

            var viewModel = new HomeViewModel()
            {
                Dishes = dishes,
                SelectedCategory = selectedCategory,
                AvailableCategories = availableCategories
            };
            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
