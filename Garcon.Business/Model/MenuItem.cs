using System.Text.Json.Serialization;
using System.Collections.Generic;
using Garcon.Business.Enum;
using System.Linq;
using System;


namespace Garcon.Business.Model
{
    public class MenuItem
    {
        private int _id;
        private decimal _price;
        private decimal _prepTimeInMins;
        private decimal _cookTimeInMins;
        private List<MenuIngredient> _menuIngredients;

        public int Id
        {
            get { return this._id; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Id value must be greater than zero.");
                }

                this._id = value;
            }
        }
        public string Name { get; set; }
        public decimal Price
        {
            get { return this._price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price value must be greater than or equal to zero.");
                }

                this._price = value;
            }
        }
        public decimal PrepTimeInMins
        {
            get { return _prepTimeInMins; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("PrepTimeInMins value must be greater than or equal to zero.");
                }

                _prepTimeInMins = value;
            }
        }
        public decimal CookTimeInMins
        {
            get { return this._cookTimeInMins; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("CookTimeInMins value must be greater than or equal to zero.");
                }

                this._cookTimeInMins = value;
            }
        }
        public bool IsChefRecommended { get; set; } = false;
        public MenuItemClassification Classification { get; set; }

        [JsonIgnore]
        public List<MenuIngredient> MenuIngredients
        {
            get { return this._menuIngredients; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("MenuIngredients cannot be null.");
                }

                this._menuIngredients = value;
            }
        }

        public MenuItem(int id)
        {
            Id = id;
        }


        /// <summary>
        /// Checks if the menu item contains the provided ingredient
        /// </summary>
        public bool IsContainingIngredient(int ingredientId)
            => MenuIngredients.Any(m => m.IngredientItemId == ingredientId);
    }
}
