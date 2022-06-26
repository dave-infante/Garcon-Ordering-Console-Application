using Garcon.Business.Repository.Interface;
using System.Collections.Generic;
using Garcon.Business.Model;
using Garcon.Business.Enum;
using System.Linq;
using System;
using Garcon.App.Controller.Abstract;


namespace Garcon.App.Controller
{
    public sealed class MenuController : BaseController
    {
        public MenuController(IUnitOfWork unitOfWork) : base(unitOfWork) { }


        /// <summary>
        /// Gets list of menu items tagged as chef recommended
        /// </summary>
        public ICollection<MenuItem> GetChefRecommendedMenuItems()
            => unitOfWork.MenuItems.GetChefRecommendedMenuItems().ToList();


        /// <summary>
        /// Gets list of menu category that is available 
        /// </summary>
        public ICollection<Tuple<int, string>> GetAvailableCategorySelectionList()
            => unitOfWork.MenuItems.GetAvailableCategorySelectionList();


        /// <summary>
        /// Gets list of menu items tagged as chef recommended
        /// </summary>
        public ICollection<MenuItem> GetByClassification(MenuItemClassification selectedMenuClassification)
            => unitOfWork.MenuItems.GetByClassification(selectedMenuClassification).ToList();


        /// <summary>
        /// Gets the menu item quantity available for ordering
        /// </summary>
        public int GetAvailabilityCount(MenuItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Argument cannot be null.");
            }

            return unitOfWork.MenuItems.GetAvailabilityCount(item);
        }


        /// <summary>
        /// Validates of a given ordered item is still available based on stock quantity
        /// </summary>
        public bool IsMenuItemAvailable(OrderItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Argument cannot be null.");
            }

            return unitOfWork.MenuItems.IsMenuItemAvailable(item);
        }


        /// <summary>
        /// Gets key names from menu item selection list
        /// </summary>
        public string GetKeyFromCategorySelectionList(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id), "Id must be a positive integer.");
            }

            return unitOfWork.MenuItems.GetKeyFromCategorySelectionList(id);
        }
    }
}
