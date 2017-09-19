using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models
{
    public class Dish
    {
        public int DishId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        public string Description { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<DishExtra> DishExtras { get; set; }
    }
}