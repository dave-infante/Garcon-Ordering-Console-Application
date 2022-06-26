using System.Collections.Generic;
using Garcon.Business.Model;
using System.Linq;
using System;
using Garcon.App.Page.Component;
using Garcon.App.Styles;
using Garcon.App.Controller;
using Garcon.App.App;
using Garcon.App.Page.Partials;
using Garcon.App.Page.Abstract;

namespace Garcon.App.Page
{
    public sealed class ShortageSupplyPage : ConsolePage
    {
        private readonly OrderController order;
        private readonly MenuController menu;

        public ShortageSupplyPage(Router p, OrderController order, MenuController menu) : base(p, typeof(ShortageSupplyPage).Name)
        {
            this.order = order;
            this.menu = menu;
        }

        protected override void ShowContent()
        {
            List<OrderItem> selectedOrderItems = new List<OrderItem>();
            List<Tuple<string, int>> ingredientsUsedFromOrderList = null;

            // GET ALL ORDERS AFFECTED BY THE SHORTAGE        

            order.SnapshotCurrentCartItems();
            var affectedOrdersDueToShortage = order.GetCartItemsAffectedFromSupplyShortage()?.ToList();
            order.Order.RemoveMultipleItemFromCart(affectedOrdersDueToShortage);

            // CASE 1: ONLY ONE ITEM HAS SUPPLY SHORTAGE          
            if (affectedOrdersDueToShortage.Count == 1)
            {
                Confirm.UpdateOrderItemQuantityByAvailability(router, order, menu, affectedOrdersDueToShortage.FirstOrDefault());
            }

            // CASE 2: AT LEAST TWO ITEMS HAVE SUPPLY SHORTAGE
            else
            {
                while (affectedOrdersDueToShortage.Count > 0)
                {
                    OrderItem orderItemSelectedToKeep = null;

                    // IF ONLY ONE ITEM IS LEFT IN THE SELECTION
                    if (affectedOrdersDueToShortage.Count == 1)
                    {
                        var singleItem = affectedOrdersDueToShortage.FirstOrDefault();

                        if (Confirm.RemainingOrderItemAffectedFromSupplyShortage(singleItem))
                        {
                            orderItemSelectedToKeep = singleItem;
                            selectedOrderItems.Add(orderItemSelectedToKeep);
                        }
                        else
                        {
                            affectedOrdersDueToShortage.Remove(singleItem);
                        }
                    }

                    // SELECT WHAT ITEMS TO CONSIDER AS ORDER
                    else
                    {
                        Tuple<OrderItem, bool> response = SupplyShortage.SelectOrderItemsAffectedFromSupplyShortage(affectedOrdersDueToShortage);

                        // ABORT PLACING OF ORDERS
                        if (response.Item2)
                        {
                            router.SetConsolePage<HomePage>();
                            order.RevertCartItemsFromShapshot();
                            Prompt.ShowPopUpMessage("Changes have been discarded. You will now be redirected to the home page.", Settings.WarningColor);
                            return;
                        }
                        else
                        {
                            orderItemSelectedToKeep = response.Item1;

                            if (orderItemSelectedToKeep != null)
                            {
                                selectedOrderItems.Add(orderItemSelectedToKeep);
                            }
                            else
                            {
                                affectedOrdersDueToShortage.Clear();
                            }
                        }
                    }

                    // PROCESS THE SELECTED ITEM AND DETERMINE IF THERE ARE AVAILABLE SUPPLY TO CATER MORE ORDERED ITEMS
                    if (orderItemSelectedToKeep != null)
                    {
                        affectedOrdersDueToShortage.Remove(orderItemSelectedToKeep);
                        ingredientsUsedFromOrderList = order.GetIngredientsUsedFromOrder(selectedOrderItems).ToList();
                        affectedOrdersDueToShortage = order.GetWhatIsAvailableFromOrderItems(affectedOrdersDueToShortage, ingredientsUsedFromOrderList)?.ToList();
                    }
                }
            }

            // CONFIRM THE CHANGES MADE BASED FROM QUANTITY
            if (selectedOrderItems.Count > 0)
            {
                selectedOrderItems.ForEach(s => order.Order.AddItemToCart(s.MenuItem, s.Quantity));

                if (order.GetCartItemSnapshot().Sum(s => s.Quantity) != order.Order.GetTotalCartItemQuantity())
                {
                    Confirm.FinalizingOrderChangesAffectedFromSupplyShortage(router, order, menu);
                }
                else
                {
                    router.SetConsolePage<HomePage>();
                    order.RevertCartItemsFromShapshot();
                    Prompt.ShowPopUpMessage("Changes have been discarded. You will now be redirected to the home page.", Settings.WarningColor);
                }
            }

            // IF THE AFFECTED ITEMS ARE DISREGARDED, FORWARD THE UNAFFECTED ORDER ITEMS
            else if (order.Order.GetCartItems().Count > 1)
            {
                Confirm.FinalizingOrderChangesAffectedFromSupplyShortage(router, order, menu);
            }
        }
    }
}
