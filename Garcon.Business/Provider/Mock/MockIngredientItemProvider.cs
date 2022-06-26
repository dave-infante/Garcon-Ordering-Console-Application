using System.Collections.Generic;
using Garcon.Business.Model;
using System;
using Garcon.Business.Provider.Interface;

namespace Garcon.Business.Provider.Mock
{
    public class MockIngredientItemProvider : IProvider<IngredientItem>
    {
        private List<IngredientItem> list;

        public MockIngredientItemProvider()
        {
            list = new List<IngredientItem>()
            {
                new IngredientItem(1) { Name = "Chicken", SupplyCount = 3  },
                new IngredientItem(2) { Name = "Soy Sauce", SupplyCount = 10  },
                new IngredientItem(3) { Name = "Tomato", SupplyCount = 5  },
                new IngredientItem(4) { Name = "Oil", SupplyCount = 2  },
                new IngredientItem(5) { Name = "Fruits", SupplyCount = 1  },
                new IngredientItem(6) { Name = "Sugar", SupplyCount = 5  },
            };
        }

        public List<IngredientItem> GetEntityFromSource()
            => list;

        public void SaveChanges(List<IngredientItem> entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            list = entity;
        }
    }
}
