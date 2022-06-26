using System.Collections.Generic;
using Garcon.Business.Model;
using System;
using Garcon.Business.Provider.Interface;

namespace Garcon.Business.Provider.Mock
{
    public class MockMenuIngredientProvider : IProvider<MenuIngredient>
    {
        private List<MenuIngredient> list;

        public MockMenuIngredientProvider()
        {
            list = new List<MenuIngredient>()
            {
                new MenuIngredient(1, 1, 2),
                new MenuIngredient(1, 2, 1),
                new MenuIngredient(2, 1, 1),
                new MenuIngredient(2, 3, 1),
                new MenuIngredient(2, 4, 1),
                new MenuIngredient(3, 1, 2),
                new MenuIngredient(3, 4, 1),
                new MenuIngredient(4, 5, 1),
                new MenuIngredient(4, 6, 1),
                new MenuIngredient(5, 6, 1)
            };
        }

        public List<MenuIngredient> GetEntityFromSource()
            => list;

        public void SaveChanges(List<MenuIngredient> entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            list = entity;
        }
    }
}
