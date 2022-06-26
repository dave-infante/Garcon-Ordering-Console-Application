using Garcon.Business.Repository.Interface;
using System.Collections.Generic;
using Garcon.Business.Model;
using Garcon.Business.Enum;
using System.Linq;
using System;
using Garcon.App.Controller.Abstract;


namespace Garcon.App.Controller
{
    public sealed class OrderController : BaseController
    {
        public MenuItemClassification SelectedMenuClassification { get; set; }
        public Order Order { get; private set; }
        private List<OrderItem> orderItemListSnapshot;

        public OrderController(IUnitOfWork unitOfWork) : base(unitOfWork) { }


        /// <summary>
        /// Request for a new order.
        /// </summary>
        public void CreateNewOrder()
            => Order = new Order();


        /// <summary>
        /// Saves the content of the cart items to a temporary snapshot variable at the time this method is invoked.
        /// </summary>
        public void SnapshotCurrentCartItems()
            => orderItemListSnapshot = Order.GetCartItems().ConvertAll(o => new OrderItem(o.MenuItem, o.Quantity));


        /// <summary>
        /// Retrieve the snapshot cart items.
        /// </summary>
        public List<OrderItem> GetCartItemSnapshot()
             => orderItemListSnapshot;


        /// <summary>
        /// Checks if there is a shortage of ingredient supply based on the cart items content.
        /// </summary>
        public bool IsIngredientSupplyShortageExist()
            => GetCartItemsAffectedFromSupplyShortage().Count() > 0;


        /// <summary>
        /// Revert the changes that have been made and retrieve the original cart items content from the temporary snapshot variable.
        /// </summary>
        public void RevertCartItemsFromShapshot()
        {
            Order.ClearCart();
            orderItemListSnapshot.ForEach(i => Order.AddItemToCart(i.MenuItem, i.Quantity));
        }


        /// <summary>
        /// Trigger the app to initiate placing of order items
        /// </summary>
        public void PlaceAllCartItemsToOrder()
        {
            if (Order.Status == OrderStatus.Cancelled || Order.Status == OrderStatus.Completed)
            {
                throw new InvalidOperationException("Operation is invalid because this order is already closed.");
            }

            if (!Order.isPlaceOrderAllowed())
            {
                throw new InvalidOperationException("Placing an order is not allowed since no order item having a status of Standby exist. Please use isPlaceOrderAllowed method for validation.");
            }


            Order.GetCartItems().ForEach(o =>
            {
                int AvailableStockCount = unitOfWork.MenuItems.GetAvailabilityCount(o.MenuItem);

                // THERE ARE ENOUGH SUPPLY OF INGREDIENTS
                if (AvailableStockCount >= o.Quantity)
                {
                    Order.PlaceOrderItem(o);
                    unitOfWork.MenuItems.PlaceOrderItem(o);
                    unitOfWork.SaveEntities();
                }
            });
        }


        /// <summary>
        /// Get cart items that have a common shortage of ingredient supply.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderItem> GetCartItemsAffectedFromSupplyShortage()
        {
            var affectedOrderItems = new List<OrderItem>();

            // GET ALL INGREDIENTS NEEDED FOR THE ORDER       
            var ingredientsRequired = Order.GetCartItems().SelectMany(o => o.MenuItem.MenuIngredients
                .Select(m => new { Ingredient = m.IngredientItem, RequiredQuantity = m.RequiredQuantity * o.Quantity, IngredientId = m.IngredientItemId }))
                .GroupBy(g => g.IngredientId)
                .Select(r => new { TotalRequired = r.Sum(m => m.RequiredQuantity), IngredientId = r.Select(m => m.IngredientId).FirstOrDefault() });

            // VALIDATE IF THE INGREDIENT DEMAND SATISFIES WITH THE AVAILABILITY SUPPLY
            ingredientsRequired?.ToList().ForEach(i =>
            {
                if (!unitOfWork.IngredientItems.IsIngredientAvailable(i.IngredientId, i.TotalRequired))
                {
                    var list = Order.GetCartItems()
                    .Where(o => o.MenuItem.IsContainingIngredient(i.IngredientId) && !affectedOrderItems
                    .Any(a => a.MenuItem == o.MenuItem));

                    if (list.Count() >= 2)
                    {
                        affectedOrderItems.AddRange(list);
                    }
                }
            });

            return affectedOrderItems;
        }


        /// <summary>
        /// Get the cart items not affected by the common shortage of ingredient items.
        /// </summary>
        public IEnumerable<OrderItem> GetCartItemsNotAffectedFromSupplyShortage()
            => Order.GetCartItems().Except(GetCartItemsAffectedFromSupplyShortage());


        /// <summary>
        /// Get order items based on the availability of ingredients supply
        /// </summary>
        public IEnumerable<OrderItem> GetWhatIsAvailableFromOrderItems(List<OrderItem> orderedItems, IEnumerable<Tuple<string, int>> qtyUsed)
        {
            if (orderedItems == null)
            {
                throw new ArgumentNullException(nameof(orderedItems), "Parameter cannot be null.");
            }

            if (qtyUsed == null)
            {
                throw new ArgumentNullException(nameof(qtyUsed), "Parameter cannot be null.");
            }

            return orderedItems.Except(orderedItems.SelectMany(oi => oi.MenuItem.MenuIngredients
                .Where(mi => oi.Quantity * mi.RequiredQuantity > mi.IngredientItem.SupplyCount - (qtyUsed.Where(qu => qu.Item1 == mi.IngredientItem.Name).FirstOrDefault()?.Item2))
                .Select(a => new { orderItem = oi })).Select(orders => orders.orderItem));
        }



        /// <summary>
        /// Get the ingredients that were used in the order
        /// </summary>
        public IEnumerable<Tuple<string, int>> GetIngredientsUsedFromOrder(List<OrderItem> orderedItems)
        {
            if (orderedItems == null)
            {
                throw new ArgumentNullException(nameof(orderedItems), "Parameter cannot be null.");
            }

            return orderedItems?.SelectMany(oi => oi.MenuItem.MenuIngredients
                .Select(mi => new { mi.IngredientItem, RequestQuantity = mi.RequiredQuantity * oi.Quantity, IngredientName = mi.IngredientItem.Name }))
                .GroupBy(g => g.IngredientName)
                .Select(ing => new Tuple<string, int>(ing.Key, ing.Sum(m => m.RequestQuantity)));
        }
    }
}
