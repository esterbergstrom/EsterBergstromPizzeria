using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InMemoryDatabase.Extensions;
using InMemoryDatabase.Models;
using InMemoryDatabase.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InMemoryDatabase.Controllers
{
    public class PaymentController : Controller
    {
        private const string CartItemsSessionKey = "_CartItems";
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
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

            var model = new PayViewModel();
            if (_signInManager.IsSignedIn(User))
            {
                var user = GetCurrentUser();
                model.FullName = user.FullName;
                model.StreetAddress = user.StreetAddress;
                model.PostalCode = user.PostalCode;
                model.PhoneNumber = user.PhoneNumber;
                model.Email = user.Email;
            }

            ViewBag.PriceSum = priceSum;

            ViewData["CurrentPage"] = "Payment";
            ViewData["Categories"] = GetAllCategories();
            ViewData["CartItems"] = GetNumberOfCartItems();

            return View(model);
        }

        [HttpPost]
        public IActionResult Pay(PayViewModel model)
        {
            HttpContext.Session.Set<List<CartItem>>(CartItemsSessionKey, new List<CartItem>());
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

        private ApplicationUser GetCurrentUser()
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.Users
                .Where(x => x.Id == userId)
                .First();

            return user;
        }
    }
}
