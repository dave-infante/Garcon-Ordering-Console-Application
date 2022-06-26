using System.Text.Json.Serialization;
using System;


namespace Garcon.Business.Model
{
    public class MenuIngredient
    {
        private int _ingredientItemId;
        private int _menuId;
        private int _requiredQuantity;
        private IngredientItem _ingredientItem;

        public int IngredientItemId
        {
            get { return this._ingredientItemId; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Id value must be greater than zero.");
                }

                this._ingredientItemId = value;
            }
        }
        public int MenuId
        {
            get { return this._menuId; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Menu Id value must be greater than zero.");
                }

                this._menuId = value;
            }
        }
        public int RequiredQuantity
        {
            get { return this._requiredQuantity; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Required quantity count value must be greater than zero.");
                }

                this._requiredQuantity = value;
            }
        }

        [JsonIgnore]
        public IngredientItem IngredientItem
        {
            get { return this._ingredientItem; }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("Ingredient item cannot be null.");
                }

                this._ingredientItem = value;
            }
        }


        public MenuIngredient(int menuId, int ingredientItemId, int requiredQuantity)
        {
            MenuId = menuId;
            IngredientItemId = ingredientItemId;
            RequiredQuantity = requiredQuantity;
        }


        /// <summary>
        /// Deduct the ingredient supply count based on the order quantity
        /// </summary>
        public void DeductIngredientSupplyByOrderQuantity(int orderQuantity)
        {
            if (orderQuantity <= 0)
            {
                throw new ArgumentException("Order quantity value cannot be less than or equal to zero.");
            }

            IngredientItem.SupplyCount -= orderQuantity * RequiredQuantity;
        }
    }
}
