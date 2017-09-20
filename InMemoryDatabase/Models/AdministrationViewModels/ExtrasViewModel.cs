using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models.AdministrationViewModels
{
    public class ExtrasViewModel
    {
        public List<Extra> Extras { get; set; }
        public Extra NewExtra { get; set; }
    }
}
