using System.Collections.Generic;
using Garcon.Business.Model;
using Garcon.Business.Enum;
using System.Linq;
using System;
using Garcon.App.Styles;
using Garcon.App.Page.Component;


namespace Garcon.App.Page.Partials
{
    /// <summary>
    /// Generates a reusable template acting as a single component to show a particular output
    /// </summary>
    static class MenuList
    {
        /// <summary>
        /// Displays a formatted list of menu items.
        /// </summary>
        public static void DisplayMenuItemList(string header, ICollection<MenuItem> menuItems)
        {
            int top = 9, left = 5, row = 1;

            if (menuItems.Count > 0)
            {
                Output.Show(header, left, top);
                top += 3;
                menuItems.ToList().ForEach(o =>
                {
                    Output.ShowAsColoredSelection(row.ToString(), $"{o.Name}", Settings.SelectionFontColor, left, top++);
                    Output.Show($"Classification: {Enum.GetName(typeof(MenuItemClassification), o.Classification)}", left + 4, top++);
                    Output.Show($"Cook Time: {Format.GetTimeUnitFormat(o.CookTimeInMins)} || Prep Time: {Format.GetTimeUnitFormat(o.PrepTimeInMins)}", left + 4, top++);
                    Output.Show($"Price: {Format.GetMoneyFormat(o.Price)}", left + 4, top++);
                    row++;
                    top++;
                });
            }

            Border.RenderPageBorder(top);
            Border.RenderNavbarBorder();
        }

        /// <summary>
        /// Displays a formatted list of ordered items with general details.
        /// </summary>
        public static void DisplayOrderItemList(string header, List<OrderItem> orderItems)
        {
            int top = 9, left = 5, row = 1;

            if (orderItems.Count > 0)
            {
                Output.Show(header, left, top);
                top += 3;
                orderItems.ForEach(o =>
                {
                    Output.ShowAsColoredSelection($"[{row}]", $"{o.MenuItem.Name}", Settings.SelectionFontColor, left, top++);
                    Output.Show($"Classification: {o.MenuItem.Classification}", left + 6, top++);
                    Output.Show($"Cook Time: {Format.GetTimeUnitFormat(o.MenuItem.CookTimeInMins)} || Prep Time: {Format.GetTimeUnitFormat(o.MenuItem.PrepTimeInMins)}", left + 6, top++);
                    Output.Show($"Price: {Format.GetMoneyFormat(o.MenuItem.Price)} || Quantity: {o.Quantity}", left + 6, top++);
                    row++;
                    top++;
                });
            }

            Border.RenderPageBorder(top);
            Border.RenderNavbarBorder();
        }

        /// <summary>
        /// Displays a formatted list of ordered items being processed.
        /// </summary>
        public static void DisplayProcessingItemList(string header, List<OrderItem> orderItems)
        {
            int top = 9, left = 5, row = 1;

            if (orderItems.Count > 0)
            {
                Output.Show(header, left, top);
                top += 3;
                orderItems.ForEach(o =>
                {
                    var timeTemplate = o.Status != OrderItemStatus.Served ? $"|| Time: {o.GetRemainingTimeString()}" : "";
                    Output.ShowAsColoredSelection($"[{row}]", $"{o.MenuItem.Name} x {o.Quantity}", Settings.SelectionFontColor, left, top++);
                    Output.Show($"CookTime: {Format.GetTimeUnitFormat(o.MenuItem.CookTimeInMins)} || PrepTime: {Format.GetTimeUnitFormat(o.MenuItem.PrepTimeInMins)}", left + 6, top++);
                    Output.Show($"Current Status: {o.Status} {timeTemplate}", left + 6, top++);
                    row++;
                    top++;
                });
            }

            Border.RenderPageBorder(top);
            Border.RenderNavbarBorder();
        }
    }
}
