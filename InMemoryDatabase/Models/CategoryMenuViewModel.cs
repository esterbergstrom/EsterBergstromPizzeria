using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models
{
    public class CategoryMenuViewModel
    {
        public Enums.DishCategory SelectedCategory { get; set; }
        public List<Enums.DishCategory> AvailableCategories { get; set; }
    }
}
