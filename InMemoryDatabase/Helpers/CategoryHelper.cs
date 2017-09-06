using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemoryDatabase.Models;

namespace InMemoryDatabase.Helpers
{
    public static class CategoryHelper
    {
        public static string EnumToString(Enums.DishCategory categoryEnum)
        {
            switch (categoryEnum)
            {
                case Enums.DishCategory.ItalianPizza:
                    return "Italienska pizzor";
                case Enums.DishCategory.SpecialPizza:
                    return "Specialpizzor";
                case Enums.DishCategory.Salad:
                    return "Sallader";
                default:
                    return string.Empty;
            }
        }

        public static List<Enums.DishCategory> EnumsToList()
        {
            var categories = new List<Enums.DishCategory>()
            {
                Enums.DishCategory.ItalianPizza,
                Enums.DishCategory.SpecialPizza,
                Enums.DishCategory.Salad
            };

            return categories;
        }
    }
}
