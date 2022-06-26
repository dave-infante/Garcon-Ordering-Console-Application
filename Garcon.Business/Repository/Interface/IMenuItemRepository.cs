using System.Collections.Generic;
using System;
using Garcon.Business.Model;
using Garcon.Business.Enum;

namespace Garcon.Business.Repository.Interface
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        void PlaceOrderItem(OrderItem orderItem);

        ICollection<MenuItem> GetChefRecommendedMenuItems();
        ICollection<MenuItem> GetByClassification(MenuItemClassification classification);
        ICollection<Tuple<int, string>> GetAvailableCategorySelectionList();

        MenuItem GetByMenuName(string menuName);
        int GetAvailabilityCount(MenuItem menuItem);
        string GetKeyFromCategorySelectionList(int id);
        bool IsChefMenuIsAvailable();
        bool IsMenuItemExist(MenuItem menuItem);
        bool IsMenuItemAvailable(OrderItem orderItem);
        bool IsMenuClassificationIsAvailable(MenuItemClassification classification);


    }
}
