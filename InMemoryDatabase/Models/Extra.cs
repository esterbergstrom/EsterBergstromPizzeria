using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models
{
    public class Extra
    {
        public int ExtraId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageURL { get; set; }

        public ICollection<DishExtra> DishExtras { get; set; }
    }
}
