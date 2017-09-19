using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Display]
        public string Name { get; set; }
    }
}
