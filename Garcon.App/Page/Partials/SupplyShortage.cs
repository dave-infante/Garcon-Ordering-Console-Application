using System.Collections.Generic;
using Garcon.Business.Model;
using Garcon.Business.Enum;
using System.Linq;
using System;
using Garcon.App.Page.Component;
using Garcon.App.Styles;


namespace Garcon.App.Page.Partials
{
    public static class SupplyShortage
    {
        /// <summary>
        /// Displays Order Item with Insufficient Stocks
        /// </summary>
        public static void ShowInsufficientMenuItem(OrderItem orderItem, int availableCount)
        {
            Screen.PrepareConsole();
            Border.RenderPageBorder();

            int top = 9, left = 5;
            Output.Show("Unfortunately, we have insufficient supply of ingredients for your ordered item:", Settings.DangerColor, left, top++);
            Output.Show("Would you like consider ordering the item given what is available from our supply?", Settings.DangerColor, left, top++);

            top += 2;
            Output.ShowAsColoredSelection($"[1]", $"{orderItem.MenuItem.Name}", Settings.SelectionFontColor, left, top++);
            Output.Show($"Classification: {Enum.GetName(typeof(MenuItemClassification), orderItem.MenuItem.Classification)}", left + 6, top++);
            Output.Show($"Ordered Quantity: {orderItem.Quantity} || Available: {availableCount}", left + 6, top++);

            Border.RenderPageBorder(top);
            Border.RenderNavbarBorder();
        }

        /// <summary>
        /// Displays a formatted list of ordered items with general details.
        /// </summary>
        public static void DisplayAffectedItemList(List<OrderItem> orderItems)
        {
            int top = 9, left = 5, row = 1;

            if (orderItems.Count > 0)
            {
                Output.Show("We have insufficient ingredient supply for these ordered items shown below.", left, top++);
                Output.Show("Please select which of these items you want to keep as your order.", left, top++);

                top += 2;
                orderItems.ForEach(o =>
                {
                    Output.ShowAsColoredSelection($"[{row}]", $"{o.MenuItem.Name}", Settings.SelectionFontColor, left, top++);
                    Output.Show($"Classification: {o.MenuItem.Classification}", left + 6, top++);
                    Output.Show($"Ordered Quantity: {o.Quantity}", left + 6, top++);
                    row++;
                    top++;
                });
            }

            Border.RenderPageBorder(top);
            Border.RenderNavbarBorder();
        }

        /// <summary>
        /// Select order items to consider against other order items with common supply having shortage. User will also has an option to abort placing of order
        /// </summary>
        public static Tuple<OrderItem, bool> SelectOrderItemsAffectedFromSupplyShortage(List<OrderItem> conflictOrders)
        {
            while (true)
            {
                Screen.PrepareConsole();
                DisplayAffectedItemList(conflictOrders);
                NavigationContent.CartItemsWithIngredientDeficiency();
                var input = Input.InputLine($"Enter here: ", 4, 3);

                switch (input)
                {
                    // CANCEL PLACE ORDER
                    case "B":
                        return new Tuple<OrderItem, bool>(null, true);

                    // NONE OF THE CHOICES
                    case "N":
                        return new Tuple<OrderItem, bool>(null, false);

                    // SELECT ORDER ITEM
                    default:
                        if (int.TryParse(input, out int enteredId))
                        {
                            OrderItem selectedOrderItem = conflictOrders.ElementAtOrDefault(enteredId - 1);
                            return Confirm.InitialSelectionOfOrderItemAffectedFromSupplyShortage(selectedOrderItem) ?
                                new Tuple<OrderItem, bool>(selectedOrderItem, false) : new Tuple<OrderItem, bool>(null, true);
                        }
                        break;
                }
            }
        }
    }
}
