using System.Collections.Generic;
using Garcon.Business.Model;
using System;
using Garcon.App.Styles;
using Garcon.App.Controller;
using Garcon.App.Page.Partials;
using Garcon.App.App;


namespace Garcon.App.Page.Component
{
    public static class Confirm
    {
        public static void GoToWelcomePage(Router router)
        {
            if (Input.ShowConfirmation($"Are you sure you want to quit?", "Proceeding will clear your current progress..."))
            {
                router.SetConsolePage<WelcomePage>();
                Prompt.ShowPopUpMessage($"See you again soon! The App will now prepare for the next customer.", Settings.SuccessColor);
            }
        }

        public static void AddItemToCart(Router router, OrderController order, MenuItem menuItem)
        {
            if (!(menuItem is null))
                if (Input.ShowConfirmation($"Are you sure you want to add {menuItem.Name}? "))
                {
                    order.Order.AddItemToCart(menuItem);
                    router.SetConsolePage<HomePage>();
                    Prompt.ShowPopUpMessage($"{menuItem.Name} has been added successfully.", Settings.SuccessColor);
                }
        }

        public static void RemoveItemFromCart(OrderController order, OrderItem orderItem)
        {
            if (Input.ShowConfirmation("Are you sure you want to remove this item? "))
            {
                order.Order.RemoveItemFromCart(orderItem.MenuItem);
                Prompt.ShowPopUpMessage("Order item has been removed from your cart successfully!", Settings.SuccessColor);
            }
        }

        public static void ClearCart(OrderController order)
        {
            if (Input.ShowConfirmation("Are you sure you want to clear your cart? "))
            {
                order.Order.ClearCart();
                Prompt.ShowPopUpMessage("Your cart list has been cleared successfully!", Settings.SuccessColor);
            }
        }
        public static void AddChefRecommendedMenu(Router handler, OrderController order, ICollection<MenuItem> list)
        {
            if (Input.ShowConfirmation($"Are you sure you want to add all {list.Count} items to your cart? "))
            {
                order.Order.AddMultipleItemToCart(list);
                Prompt.ShowPopUpMessage("The Chef's recommended menu has been added to your cart successfully!", Settings.SuccessColor);
            }

            handler.SetConsolePage<HomePage>();
        }

        public static void PlaceOrder(Router router, OrderController order)
        {
            if (Input.ShowConfirmation("Are you sure you want to place these items to order?", "Proceeding will start the preperation of your orders..."))
                if (order.IsIngredientSupplyShortageExist())
                {
                    Prompt.ShowPopUpMessage("Unfortunately, there have been ingredient supply shortage for your ordered items.", "We will now assist you to modify your orders based on our current supply.", Settings.DangerColor);
                    router.SetConsolePage<ShortageSupplyPage>();
                }
                else
                {
                    order.PlaceAllCartItemsToOrder();
                    router.SetConsolePage<OrderListPage>();
                    Prompt.ShowPopUpMessage("Your order has been placed successfully!", Settings.SuccessColor);
                }
        }

        public static void UpdateOrderItemQuantityByAvailability(Router router, OrderController order, MenuController menu, OrderItem orderItem)
        {
            var availableCount = menu.GetAvailabilityCount(orderItem.MenuItem);
            SupplyShortage.ShowInsufficientMenuItem(orderItem, availableCount);

            if (Input.ShowConfirmation("Proceeding will use what is available from our supply."))
            {
                order.Order.GetCartItems().ForEach(c =>
                {
                    if (c.MenuItem.Id == orderItem.MenuItem.Id)
                    {
                        c.Quantity = menu.GetAvailabilityCount(c.MenuItem);
                    }
                });
            }
            else if (order.Order.GetCartItems().Count > 1)
            {
                order.Order.RemoveItemFromCart(orderItem.MenuItem);
            }
            else
            {
                router.SetConsolePage<HomePage>();
                order.RevertCartItemsFromShapshot();
                Prompt.ShowPopUpMessage("Changes have been discarded. You will now be redirected to the home page.", Settings.WarningColor);
            }
        }

        public static void CancelOrder(Router router, OrderController order)
        {
            if (Input.ShowConfirmation("Are you sure you want to cancel your order?", "Proceeding will clear your current progress..."))
            {
                order.Order.CancelOrder();
                router.SetConsolePage<WelcomePage>();
                Prompt.ShowPopUpMessage($"You have cancelled your order successfully! The App will now prepare orders for the next customer...", Settings.SuccessColor);
            }
        }

        public static void Billout(Router router, OrderController order)
        {
            if (Input.ShowConfirmation("Are you sure you want to confirm order bill-out?", "Proceeding will end your order transaction..."))
            {
                order.Order.BillOut();
                router.SetConsolePage<WelcomePage>();

                do BillOut.ShowSummary(order);
                while (Console.ReadKey().Key != ConsoleKey.Enter);

                Prompt.ShowPopUpMessage($"The App will now prepare orders for the next customer...", Settings.InfoColor);
            }
        }

        public static bool RemainingOrderItemAffectedFromSupplyShortage(OrderItem orderItem)
        {
            Screen.PrepareConsole();
            MenuList.DisplayOrderItemList("Here is the remaining available order you may still add: ", new List<OrderItem>() { orderItem });
            return Input.ShowConfirmation("Are you sure you want to add this remaining order item? ");
        }

        public static void FinalizingOrderChangesAffectedFromSupplyShortage(Router router, OrderController order, MenuController menu)
        {
            Screen.PrepareConsole();
            MenuList.DisplayOrderItemList("Here is the new list of orders you have updated:", order.Order.GetCartItems());

            if (Input.ShowConfirmation("Are you sure you want to proceed with these changes? "))
            {
                order.Order.GetCartItems().ForEach(item =>
                {
                    if (!menu.IsMenuItemAvailable(item))
                    {
                        UpdateOrderItemQuantityByAvailability(router, order, menu, item);
                    }
                });

                router.SetConsolePage<OrderListPage>();
                order.PlaceAllCartItemsToOrder();
                Prompt.ShowPopUpMessage("Your orders have been updated successfully! We will now process your orders...", Settings.SuccessColor);
            }
            else
            {
                router.SetConsolePage<HomePage>();
                order.RevertCartItemsFromShapshot();
                Prompt.ShowPopUpMessage("Changes have been discarded. You will now be redirected to the home page.", Settings.WarningColor);
            }
        }

        public static bool InitialSelectionOfOrderItemAffectedFromSupplyShortage(OrderItem orderItem)
        {
            if (!(orderItem is null))
            {
                Screen.PrepareConsole();
                MenuList.DisplayOrderItemList("You have selected this item to keep as your order:", new List<OrderItem>() { orderItem });
                return Input.ShowConfirmation("Are you sure you want to keep this item in your order? ");
            }

            return false;
        }
    }
}
