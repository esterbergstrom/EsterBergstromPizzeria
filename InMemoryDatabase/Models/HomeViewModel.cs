using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models
{
    public class HomeViewModel
    {
        public List<Dish> Dishes { get; set; }

        public Category SelectedCategory { get; set; }
        public List<Category> AvailableCategories { get; set; }
    }
}
