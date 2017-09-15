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
using Microsoft.EntityFrameworkCore;

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

            dish.DishExtras = _context.DishExtras
                .Include(x => x.Extra)
                .Where(x => x.Dish == dish)
                .ToList();

            var selectableExtras = new List<SelectListItem>();

            foreach (var dishExtra in dish.DishExtras)
            {
                var extra = new SelectListItem()
                {
                    Text = dishExtra.Extra.Name,
                    Value = dishExtra.Extra.ExtraId.ToString()
                };
                selectableExtras.Add(extra);
            }

            var cartItem = new CartItem()
            {
                Dish = dish,
                AvailableExtras = selectableExtras
            };

            ViewData["CurrentPage"] = dish.Name;
            ViewData["Categories"] = GetAllCategories();
            ViewData["CartItems"] = GetNumberOfCartItems();

            return View(cartItem);
        }

        [HttpPost]
        public IActionResult CustomizeDish(CartItem cartItem)
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>(CartItemsSessionKey);
            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
            }

            cartItem.Dish = _context.Dishes
                .Where(x => x.DishId == cartItem.Dish.DishId)
                .First();

            if (cartItem.SelectedExtras != null)
            {
                cartItem.Extras = new List<Extra>();
                foreach (var selectedExtra in cartItem.SelectedExtras)
                {
                    var extra = _context.Extras
                        .Where(x => x.ExtraId == int.Parse(selectedExtra))
                        .First();
                    cartItem.Extras.Add(extra);
                }
            }

            cartItems.Add(cartItem);
            HttpContext.Session.Set<List<CartItem>>(CartItemsSessionKey, cartItems);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>(CartItemsSessionKey);

            if (cartItems == null)
            {
                return RedirectToAction("Index");
            }

            ViewData["CurrentPage"] = "Cart";
            ViewData["Categories"] = GetAllCategories();
            ViewData["CartItems"] = GetNumberOfCartItems();

            return View(cartItems);
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
