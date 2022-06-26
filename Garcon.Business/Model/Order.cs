using System.Collections.Generic;
using Garcon.Business.Enum;
using System.Linq;
using System;


namespace Garcon.Business.Model
{
    public class Order
    {
        private int _id;
        private List<OrderItem> orderItems = new List<OrderItem>();
        private decimal taxPercentage = 0.12M;

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
        public DateTime? DateOrdered { get; private set; } = null;
        public OrderStatus Status { get; private set; } = OrderStatus.Standby;
        public decimal SubTotalCost { get => TotalCost * (1 - taxPercentage); }
        public decimal TaxCost { get => TotalCost * taxPercentage; }
        public decimal TotalCost { get => GetOrderedItems().Sum(o => o.Quantity * o.MenuItem.Price); }
        public decimal ServiceCharge { get => (0.10M * TotalCost); }


        /// <summary>
        /// Retrieve the current list of order items. 
        /// </summary>
        public List<OrderItem> GetOrderedItems()
            => this.orderItems.Where(o => o.Status != OrderItemStatus.Standby).ToList();


        /// <summary>
        /// Get ordered items per quantity
        /// </summary>
        public int GetTotalOrderItemQuantity()
            => GetOrderedItems().Sum(i => i.Quantity);


        /// <summary>
        /// Retrieve the current list of cart items. 
        /// </summary>
        public List<OrderItem> GetCartItems()
            => this.orderItems.Where(o => o.Status == OrderItemStatus.Standby).ToList();


        public int GetTotalCartItemQuantity()
            => GetCartItems().Sum(i => i.Quantity);


        /// <summary>
        /// Add a single menu item to the cart list.
        /// </summary>
        public void AddItemToCart(MenuItem item, int Qty = 1)
        {
            if (item is null)
            {
                throw new ArgumentNullException("Parameter cannot be null");
            }

            this.ValidateIfOrderIsClosed();
            OrderItem orderItem = this.orderItems.FirstOrDefault(o => o.MenuItem.Id == item.Id && o.Status == OrderItemStatus.Standby);

            if (orderItem is null)
            {
                orderItems.Add(new OrderItem(item, Qty));
            }
            else
            {
                orderItem.AddQuantity();
            }
        }


        /// <summary>
        /// Add multiple menu items to the cart list.
        /// </summary>
        public void AddMultipleItemToCart(IEnumerable<MenuItem> items)
        {
            if (items is null || items?.Count() == 0)
            {
                throw new ArgumentNullException("Parameter cannot be empty or null");
            }

            this.ValidateIfOrderIsClosed();
            items.ToList().ForEach(o => AddItemToCart(o));
        }


        /// <summary>
        /// Subtract order item quantity from cart
        /// </summary>
        public void SubtractItemFromCart(MenuItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException("Parameter cannot be null");
            }

            this.ValidateIfOrderIsClosed();
            OrderItem orderItem = orderItems.FirstOrDefault(o => o.MenuItem.Id == item.Id && o.Status == OrderItemStatus.Standby);

            if (orderItem is null)
            {
                throw new ArgumentException($"Order item with Menu Id {item.Id} not found.");
            }
            else if (orderItem.Quantity == 1)
            {
                orderItems.Remove(orderItem);
            }
            else
            {
                orderItem.SubtractQuantity();
            }
        }


        /// <summary>
        /// Remove multiple existing items from cart list.
        /// </summary>
        public void RemoveMultipleItemFromCart(List<OrderItem> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException("Parameter cannot be null");
            }

            this.ValidateIfOrderIsClosed();
            items.ForEach(i => RemoveItemFromCart(i.MenuItem));
        }


        /// <summary>
        /// Remove an existing order item in the cart list.
        /// </summary>
        public void RemoveItemFromCart(MenuItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException("Parameter cannot be null");
            }

            this.ValidateIfOrderIsClosed();
            OrderItem orderItem = orderItems.FirstOrDefault(o => o.MenuItem.Id == item.Id && o.Status == OrderItemStatus.Standby);

            if (orderItem is null)
            {
                throw new ArgumentException($"Order item with Menu Id {item.Id} not found.");
            }

            orderItems.Remove(orderItem);
        }


        /// <summary>
        /// Clears the cart list.
        /// </summary>
        public void ClearCart()
        {
            this.ValidateIfOrderIsClosed();

            orderItems = new List<OrderItem>(orderItems.Where(o => o.Status != OrderItemStatus.Standby));
        }


        /// <summary>
        /// Sets the order to processing phase. This will also commence the preperation time to the ordered items.
        /// </summary>
        public void PlaceOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new NullReferenceException("Order item value cannot be null.");
            }

            this.ValidateIfOrderIsClosed();

            if (!isPlaceOrderAllowed())
            {
                throw new InvalidOperationException("Placing an order is not allowed since no order item having a status of Standby exist. Please use isPlaceOrderAllowed method for validation.");
            }

            Status = Status != OrderStatus.Processing ? OrderStatus.Processing : Status;
            orderItem.StartPrepare();
        }


        /// <summary>
        /// Set the order to cancelled. This will end the order transaction with the user.
        /// </summary>
        public void CancelOrder()
        {
            this.ValidateIfOrderIsClosed();

            if (!isCancelOrderAllowed())
            {
                throw new InvalidOperationException("Order cancellation is not allowed since there is an on-going processing of order items. Please use isCancelOrderAllowed method for validation.");
            }

            Status = OrderStatus.Cancelled;
        }


        /// <summary>
        /// Set the order to billed-out. This will end the order transaction with the user.
        /// </summary>
        public void BillOut()
        {
            this.ValidateIfOrderIsClosed();

            if (!isBillOutAllowed())
            {
                throw new InvalidOperationException("Bill out is not allowed since it is required for all order items to be served first. Please use isBilOutAllowed method for validation.");
            }

            Status = OrderStatus.Completed;
            DateOrdered = DateTime.Now;
        }


        /// <summary>
        /// Check if  placing an order is allowed.
        /// </summary>
        public bool isPlaceOrderAllowed()
            => orderItems.Any(o => o.Status == OrderItemStatus.Standby) && (Status == OrderStatus.Standby || Status == OrderStatus.Processing);


        /// <summary>
        /// Check if bill-out an order is allowed.
        /// </summary>
        public bool isBillOutAllowed()
            => orderItems.All(o => o.Status == OrderItemStatus.Served) && Status == OrderStatus.Processing;


        /// <summary>
        /// Check if cancelling an order is allowed.
        /// </summary>
        public bool isCancelOrderAllowed()
            => orderItems.All(o => o.Status == OrderItemStatus.Standby) & Status == OrderStatus.Standby;


        /// <summary>
        /// Throws an exception if the current instance of the order is already cancelled or completed
        /// </summary>
        private void ValidateIfOrderIsClosed()
        {
            if (Status == OrderStatus.Cancelled || Status == OrderStatus.Completed)
            {
                throw new InvalidOperationException("Operation is invalid because this order is already closed.");
            }
        }
    }
}
