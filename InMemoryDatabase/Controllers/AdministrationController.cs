using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InMemoryDatabase.Data;
using InMemoryDatabase.Extensions;
using InMemoryDatabase.Models;
using Microsoft.AspNetCore.Identity;
using InMemoryDatabase.Models.AdministrationViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

            var model = new CategoriesViewModel()
            {
                Categories = _context.Categories.ToList(),
                NewCategory = new Category()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateCategory(Category newCategory)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            var category = new Category()
            {
                Name = newCategory.Name
            };
            _context.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Categories");
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

        [HttpGet]
        public IActionResult Dishes()
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["CurrentPage"] = "Dishes";
            ViewData["Categories"] = GetAllCategories();
            ViewData["CartItems"] = GetNumberOfCartItems();

            var categories = new SelectList(_context.Categories.ToList(), "CategoryId", "Name");

            var dishViewModels = new List<DishViewModel>();
            foreach (var dish in _context.Dishes.Include(x => x.Category).ToList())
            {
                var dishViewModel = new DishViewModel()
                {
                    Dish = dish,
                    Categories = categories
                };
                dishViewModels.Add(dishViewModel);
            }

            var model = new DishesViewModel()
            {
                DishViewModels = dishViewModels,
                NewDish = new Dish(),
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateDish(Dish newDish)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }
            
            _context.Dishes.Add(newDish);
            _context.SaveChanges();

            return RedirectToAction("Dishes");
        }

        [HttpPost]
        public IActionResult EditDish(int dishId,
            string name,
            decimal price,
            string description,
            string imageURL,
            int categoryId)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            var dish = _context.Dishes.Single(x => x.DishId == dishId);
            dish.Name = name;
            dish.Price = price;
            dish.Description = description;
            dish.ImageURL = imageURL;
            dish.Category = _context.Categories.Single(x => x.CategoryId == categoryId);
            _context.SaveChanges();

            return RedirectToAction("Dishes");
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
