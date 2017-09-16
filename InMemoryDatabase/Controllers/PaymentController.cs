using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InMemoryDatabase.Extensions;
using InMemoryDatabase.Models;
using InMemoryDatabase.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InMemoryDatabase.Controllers
{
    public class PaymentController : Controller
    {
        private const string CartItemsSessionKey = "_CartItems";
        private ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
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

        [HttpGet]
        public IActionResult Pay()
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>(CartItemsSessionKey);

            decimal priceSum = 0;
            foreach (var cartItem in cartItems)
            {
                priceSum += cartItem.Dish.Price;

                if (cartItem.Extras != null)
                {
                    foreach (var extra in cartItem.Extras)
                    {
                        priceSum += extra.Price;
                    }
                }
            }

            ViewBag.PriceSum = priceSum;

            ViewData["CurrentPage"] = "Payment";
            ViewData["Categories"] = GetAllCategories();
            ViewData["CartItems"] = GetNumberOfCartItems();

            return View(new BillingInformation());
        }

        [HttpPost]
        public IActionResult Pay(BillingInformation billingInformation)
        {
            return RedirectToAction("Index", "Home");
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
