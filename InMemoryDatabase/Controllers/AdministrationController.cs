using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InMemoryDatabase.Data;
using InMemoryDatabase.Extensions;
using InMemoryDatabase.Models;
using Microsoft.AspNetCore.Identity;

namespace InMemoryDatabase.Controllers
{
    public class AdministrationController : Controller
    {
        private const string CartItemsSessionKey = "_CartItems";
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdministrationController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("Categories");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Categories()
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["CurrentPage"] = "Categories";
            ViewData["Categories"] = GetAllCategories();
            ViewData["CartItems"] = GetNumberOfCartItems();

            var categories = _context.Categories.ToList();

            return View(categories);
        }

        [HttpPost]
        public IActionResult EditCategory(int categoryId, string name)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }
            
            var category = _context.Categories.Single(x => x.CategoryId == categoryId);
            category.Name = name;
            _context.SaveChanges();

            return RedirectToAction("Categories");
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

        private int GetNumberOfCartItems()
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>(CartItemsSessionKey);
            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
            }

            return cartItems.Count;
        }
    }
}
