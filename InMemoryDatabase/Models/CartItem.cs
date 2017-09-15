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
        public List<Extra> Extras { get; set; }

        public List<SelectListItem> AvailableExtras { get; set; }
        public List<string> SelectedExtras { get; set; }
    }
}
