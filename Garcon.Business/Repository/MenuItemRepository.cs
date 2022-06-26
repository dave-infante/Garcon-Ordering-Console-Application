using Garcon.Business.Repository.Interface;
using Garcon.Business.Provider.Interface;
using System.Collections.Generic;
using Garcon.Business.Helper;
using Garcon.Business.Model;
using Garcon.Business.Enum;
using System.Linq;
using System;


namespace Garcon.Business.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private IMenuIngredientRepository _menuItemRepository;

        public MenuItemRepository(IProvider<MenuItem> provider, IMenuIngredientRepository menuIngredientRepository) : base(provider)
        {
            if (menuIngredientRepository is null)
            {
                throw new ArgumentNullException("MenuIngredientRepository cannot be null");
            }

            _menuItemRepository = menuIngredientRepository;
        }

        public override ICollection<MenuItem> GetEntityList()
            => Mapper.MenuIngredientsToMenuItems(base.GetEntityList(), _menuItemRepository.GetEntityList());

        public void PlaceOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException("Order item cannot be null.");
            }

            if (!IsMenuItemExist(orderItem.MenuItem))
            {
                throw new ArgumentException("Menu item does not exist.");
            }

            if (!IsMenuItemAvailable(orderItem))
            {
                throw new InvalidOperationException("Order quantity cannot be less than the available stock count.");
            }

            var qEntity = Mapper.MenuIngredientsToMenuItems(base.GetEntityList(), _menuItemRepository.GetEntityList());
            qEntity.FirstOrDefault(e => e.Id == orderItem.MenuItem.Id)?.MenuIngredients.ForEach(o =>
            {
                o.DeductIngredientSupplyByOrderQuantity(orderItem.Quantity);
                _menuItemRepository.UpdateEntity(o);
            });
        }

        public int GetAvailabilityCount(MenuItem menuItem)
        {
            if (menuItem == null)
            {
                throw new ArgumentNullException("Order item cannot be null.");
            }

            if (!IsMenuItemExist(menuItem))
            {
                throw new ArgumentException("Menu item does not exist.");
            }

            return GetEntityList().FirstOrDefault(m => m.Id == menuItem.Id)
                .MenuIngredients.Min(m => m.IngredientItem.SupplyCount / m.RequiredQuantity);
        }

        public bool IsMenuItemAvailable(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException("Order item cannot be null.");
            }

            return GetAvailabilityCount(orderItem.MenuItem) >= orderItem.Quantity;
        }

        public bool IsMenuItemExist(MenuItem menuItem)
        {
            if (menuItem == null)
            {
                throw new ArgumentNullException("Menu item cannot be null.");
            }

            return GetEntityList().Any(o => o.Id == menuItem.Id);
        }

        public ICollection<MenuItem> GetChefRecommendedMenuItems()
            => GetEntityList().Where(e => e.IsChefRecommended && GetAvailabilityCount(e) > 0).ToList();

        public ICollection<MenuItem> GetByClassification(MenuItemClassification classification)
            => GetEntityList().Where(e => e.Classification == classification && GetAvailabilityCount(e) > 0).ToList();

        public bool IsMenuClassificationIsAvailable(MenuItemClassification classification)
            => GetEntityList().Any(e => e.Classification == classification && GetAvailabilityCount(e) > 0);

        public bool IsChefMenuIsAvailable()
            => GetEntityList().Any(e => e.IsChefRecommended && GetAvailabilityCount(e) > 0);

        public override void UpdateEntity(MenuItem Entity)
        {
            if (Entity is null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            entity.ToList().ForEach(e => { if (e.Id != Entity.Id) e = Entity; });
        }

        public MenuItem GetByMenuName(string menuName)
        {
            if (string.IsNullOrEmpty(menuName))
            {
                throw new ArgumentNullException("Entity cannot be empty or null");
            }

            return GetEntityList().FirstOrDefault(m => m.Name == menuName);
        }

        public ICollection<Tuple<int, string>> GetAvailableCategorySelectionList()
        {
            List<Tuple<int, string>> selections = new List<Tuple<int, string>>();

            if (IsChefMenuIsAvailable())
            {
                selections.Add(new Tuple<int, string>(selections.Count + 1, nameof(MenuItem.IsChefRecommended)));
            }

            if (IsMenuClassificationIsAvailable(MenuItemClassification.Appetizer))
            {
                selections.Add(new Tuple<int, string>(selections.Count + 1, nameof(MenuItemClassification.Appetizer)));
            }

            if (IsMenuClassificationIsAvailable(MenuItemClassification.MainCourse))
            {
                selections.Add(new Tuple<int, string>(selections.Count + 1, nameof(MenuItemClassification.MainCourse)));
            }

            if (IsMenuClassificationIsAvailable(MenuItemClassification.Dessert))
            {
                selections.Add(new Tuple<int, string>(selections.Count + 1, nameof(MenuItemClassification.Dessert)));
            }

            if (IsMenuClassificationIsAvailable(MenuItemClassification.Drink))
            {
                selections.Add(new Tuple<int, string>(selections.Count + 1, nameof(MenuItemClassification.Drink)));
            }

            return selections;
        }

        public string GetKeyFromCategorySelectionList(int id)
            => GetAvailableCategorySelectionList().FirstOrDefault(k => k.Item1 == id)?.Item2 ?? "";
    }
}