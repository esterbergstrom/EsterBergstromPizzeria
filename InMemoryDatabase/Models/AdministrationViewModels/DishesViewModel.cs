using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models.AdministrationViewModels
{
    public class DishesViewModel
    {
        public List<Dish> Dishes { get; set; }
        public Dish NewDish { get; set; }
    }
}
