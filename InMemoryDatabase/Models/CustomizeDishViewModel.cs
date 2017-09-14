using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models
{
    public class CustomizeDishViewModel
    {
        public Dish Dish { get; set; }
        public List<Extra> Extras { get; set; }
    }
}
