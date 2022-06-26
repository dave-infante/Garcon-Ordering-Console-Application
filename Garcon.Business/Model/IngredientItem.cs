using System;

namespace Garcon.Business.Model
{
    public class IngredientItem
    {
        private int _id;
        private int _supplyCount;

        public int Id
        {
            get { return _id; }
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
        public int SupplyCount
        {
            get { return _supplyCount; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Stock count value must be greater than or equal to zero.");
                }

                this._supplyCount = value;
            }
        }

        public IngredientItem(int id)
        {
            Id = id;
        }
    }
}
