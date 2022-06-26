using Garcon.Business.Repository.Interface;
using System;


namespace Garcon.Business.Repository
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        IMenuIngredientRepository _menuIngredients;
        IIngredientItemRepository _ingredientItems;
        IMenuItemRepository _menuItems;

        public IMenuIngredientRepository MenuIngredients
        {
            get => this._menuIngredients;
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("MenuIngredients repository cannot be null");
                }

                this._menuIngredients = value;
            }
        }
        public IIngredientItemRepository IngredientItems
        {
            get => this._ingredientItems;
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("IngredientItems repository cannot be null");
                }

                this._ingredientItems = value;
            }
        }
        public IMenuItemRepository MenuItems
        {
            get => this._menuItems;
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("MenuItems repository cannot be null");
                }

                this._menuItems = value;
            }
        }

        public UnitOfWork(IMenuIngredientRepository menuIngredients, IIngredientItemRepository ingredientItems, IMenuItemRepository menuItems)
        {
            IngredientItems = ingredientItems;
            MenuIngredients = menuIngredients;
            MenuItems = menuItems;
        }

        /// <summary>
        /// Save all repository entity to data source
        /// </summary>
        public void SaveEntities()
        {
            MenuIngredients.SaveEntity();
            IngredientItems.SaveEntity();
            MenuItems.SaveEntity();
        }
    }
}
