using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models.AdministrationViewModels
{
    public class CategoriesViewModel
    {
        public List<Category> Categories { get; set; }
        public Category NewCategory { get; set; }
    }
}
