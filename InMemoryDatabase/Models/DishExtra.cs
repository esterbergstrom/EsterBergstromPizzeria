using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models
{
    public class DishExtra
    {
        public int DishId { get; set; }
        public Dish Dish { get; set; }

        public int ExtraId { get; set; }
        public Extra Extra { get; set; }
    }
}
