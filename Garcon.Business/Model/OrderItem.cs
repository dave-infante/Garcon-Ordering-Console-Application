using System.Threading.Tasks;
using Garcon.Business.Enum;
using System;


namespace Garcon.Business.Model
{
    public class OrderItem
    {
        private int _id;
        private int _quantity;
        private MenuItem _menuItem;

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
        public MenuItem MenuItem
        {
            get { return this._menuItem; }
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("Menu item cannot be null.");
                }

                this._menuItem = value;
            }
        }
        public int Quantity
        {
            get { return this._quantity; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Quantity value must be greater than zero.");
                }

                this._quantity = value;
            }
        }
        public OrderItemStatus Status { get; private set; } = OrderItemStatus.Standby;
        public DateTime? OrderPlacedDateTime { get; private set; } = null;


        /// <summary>
        /// This will contain the details of the ordered items
        /// </summary>
        public OrderItem(MenuItem menuItem, int quantity = 1)
        {
            MenuItem = menuItem;
            Quantity = quantity;
        }


        /// <summary>
        /// Add the ordered item's quantity.
        /// </summary>
        public void AddQuantity(int quantity = 1) 
            => Quantity += quantity;


        /// <summary>
        /// Reduce the ordered item's quantity.
        /// </summary>
        public void SubtractQuantity(int quantity = 1)
        {
            if (Quantity <= 1)
            {
                throw new InvalidOperationException("Cannot subtract order item quantity because the lowest value allowed is one.");
            }

            if (Quantity <= quantity)
            {
                throw new InvalidOperationException("The provided quantity cannot be greater than or equal to the item's current quantity.");
            }

            Quantity -= quantity;
        }


        /// <summary>
        /// Initiate the preperation process of this order item.
        /// </summary>
        public void StartPrepare()
        {
            if (Status != OrderItemStatus.Standby)
            {
                throw new InvalidOperationException("Order item can only be prepared in standby status.");
            }

            OrderPlacedDateTime = DateTime.Now;
            PrepareOrder().ConfigureAwait(false);
        }


        /// <summary>
        /// Gets the remaining time of the order item since it was placed.
        /// </summary>
        /// <returns></returns>
        public string GetRemainingTimeString()
        {
            if (!isProcessing())
            {
                return "---";
            }

            decimal totalWait = (MenuItem.CookTimeInMins + MenuItem.PrepTimeInMins) * 60;
            decimal totalSeconds = totalWait - (decimal)(DateTime.Now - OrderPlacedDateTime.Value).TotalSeconds;
            int secs = (int)totalSeconds % 60, mins = (int)totalSeconds / 60;

            return mins > 0 ? $"{mins}:{secs.ToString("00")}s remaining" : mins == 0 && secs > 0 ? $"{secs}s remaining" : "---";
        }


        /// <summary>
        /// Check if this order item is currently in processing phase.
        /// </summary>
        public bool isProcessing()
            => Status != OrderItemStatus.Standby && OrderPlacedDateTime != null;


        /// <summary>
        /// Simulate the preperation process of the order item
        /// </summary>
        private async Task PrepareOrder()
        {
            Status = OrderItemStatus.Preparing;
            await Task.Delay(TimeSpan.FromSeconds(Convert.ToDouble(MenuItem.PrepTimeInMins * 60)));
            await CookOrder();
        }


        /// <summary>
        /// Simulate the cooking process of the order item
        /// </summary>
        private async Task CookOrder()
        {
            Status = OrderItemStatus.Cooking;
            await Task.Delay(TimeSpan.FromSeconds(Convert.ToDouble(MenuItem.CookTimeInMins * 60)));
            Status = OrderItemStatus.Served;
        }
    }
}
