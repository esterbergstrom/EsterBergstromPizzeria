using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models
{
    public class CartItem
    {
        public Dish Dish { get; set; }

        // Extras with a reference to Dish
        public List<Extra> Extras { get; set; }
        // Extras selected by the user
        public List<Extra> SelectedExtras { get; set; }
    }
}
