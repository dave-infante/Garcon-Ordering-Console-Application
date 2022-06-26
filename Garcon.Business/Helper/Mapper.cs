using System.Collections.Generic;
using Garcon.Business.Model;
using System.Linq;
using System;


namespace Garcon.Business.Helper
{
    public static class Mapper
    {
        public static ICollection<MenuItem> MenuIngredientsToMenuItems(ICollection<MenuItem> menuItems, ICollection<MenuIngredient> menuIngredients)
        {
            if (menuItems is null)
            {
                throw new ArgumentNullException("MenuItems cannot be null");
            }

            if (menuIngredients is null)
            {
                throw new ArgumentNullException("MenuIngredients cannot be null");
            }

            menuItems.ToList().ForEach(m => m.MenuIngredients = menuIngredients.Where(i
                => i.MenuId == m.Id)?.ToList() ?? new List<MenuIngredient>());

            return menuItems;
        }

        public static ICollection<MenuIngredient> IngredientItemToMenuIngredients(ICollection<MenuIngredient> menuIngredients, ICollection<IngredientItem> ingredientItems)
        {
            if (ingredientItems is null)
            {
                throw new ArgumentNullException("IngredientItems cannot be null");
            }

            if (menuIngredients is null)
            {
                throw new ArgumentNullException("MenuIngredients cannot be null");
            }

            menuIngredients.ToList().ForEach(m => m.IngredientItem = ingredientItems.FirstOrDefault(i
                => i.Id == m.IngredientItemId));

            return menuIngredients;
        }
    }
}
