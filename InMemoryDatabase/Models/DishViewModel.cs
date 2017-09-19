using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models
{
    public class DishViewModel
    {
        public Dish Dish { get; set; }
        public SelectList Categories { get; set; }
    }
}
