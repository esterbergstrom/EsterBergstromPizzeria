using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InMemoryDatabase.Models;
using InMemoryDatabase.Data;
using Microsoft.AspNetCore.Http;
using InMemoryDatabase.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InMemoryDatabase.Controllers
{
    public class HomeController : Controller
    {
        private const string CartItemsSessionKey = "_CartItems";
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
            ViewData["CartItems"] = GetNumberOfCartItems();

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
            ViewData["CartItems"] = GetNumberOfCartItems();

            return View(dishes);
        }

        [HttpGet]
        public IActionResult CustomizeDish(int dishId)
        {
            var dish = _context.Dishes
                .Where(x => x.DishId == dishId)
                .First();

            var extras = _context.DishExtras
                .Where(x => x.Dish == dish)
                .Select(x => x.Extra)
                .ToList();

            var model = new CartItem()
            {
                Dish = dish,
                Extras = extras
            };

            ViewData["CurrentPage"] = dish.Name;
            ViewData["Categories"] = GetAllCategories();
            ViewData["CartItems"] = GetNumberOfCartItems();

            return View(model);
        }

        [HttpPost]
        public IActionResult CustomizeDish(CartItem cartItem)
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>(CartItemsSessionKey);
            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
            }

            cartItems.Add(cartItem);

            HttpContext.Session.Set<List<CartItem>>(CartItemsSessionKey, cartItems);

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
