using System.Collections.Generic;
using Garcon.Business.Model;
using Garcon.Business.Enum;
using System;
using Garcon.Business.Provider.Interface;

namespace Garcon.Business.Provider.Mock
{
    public class MockMenuItemProvider : IProvider<MenuItem>
    {
        private List<MenuItem> list;

        public MockMenuItemProvider()
        {
            list = new List<MenuItem>()
            {
                new MenuItem(1)
                {
                    Name = "Chicken Adobo",
                    Classification = MenuItemClassification.MainCourse,
                    PrepTimeInMins = 0.05m,
                    CookTimeInMins = 0.05m,
                    Price = 75,
                    IsChefRecommended = false
                },
                new MenuItem(2)
                {
                    Name = "Afritada",
                    Classification = MenuItemClassification.MainCourse,
                    PrepTimeInMins = 0.05m,
                    CookTimeInMins = 0.05m,
                    Price = 75,
                    IsChefRecommended = true
                },
                new MenuItem(3)
                {
                        Name = "Fried Chicken",
                        Classification = MenuItemClassification.MainCourse,
                        PrepTimeInMins = 0.05m,
                        CookTimeInMins = 0.05m,
                        Price = 75,
                        IsChefRecommended = false
                },
                new MenuItem(4)
                {
                        Name = "Halo-Halo",
                        Classification = MenuItemClassification.Dessert,
                        PrepTimeInMins = 0.05m,
                        CookTimeInMins = 0,
                        Price = 75,
                        IsChefRecommended = true
                },
                new MenuItem(5)
                {
                        Name = "Coke",
                        Classification = MenuItemClassification.Drink,
                        PrepTimeInMins = 0,
                        CookTimeInMins = 0,
                        Price = 75,
                        IsChefRecommended = true
                }
            };
        }

        public List<MenuItem> GetEntityFromSource()
            => list;

        public void SaveChanges(List<MenuItem> entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            list = entity;
        }
    }
}
