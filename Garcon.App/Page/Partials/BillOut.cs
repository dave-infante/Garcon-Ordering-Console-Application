using System;
using Garcon.App.Styles;
using Garcon.App.Controller;
using Garcon.App.Page.Component;


namespace Garcon.App.Page.Partials
{
    public static class BillOut
    {
        /// <summary>
        /// Displays the billout summary of the order
        /// </summary>
        public static void ShowSummary(OrderController controller)
        {
            Screen.PrepareConsole();

            int top = 3, left = 5, row = 1;
            var orderItems = controller.Order.GetOrderedItems();

            Output.Show($"Thank you for dining at Garçon!", top++);
            Output.Show("This is the bill-out order summary for your reference", top++);
            Output.Show($"Date Ordered: {controller.Order.DateOrdered.ToString()}", top++);

            top += 3;
            orderItems.ForEach(o =>
            {
                Output.ShowAsColoredSelection($"{row}", $"{o.MenuItem.Name}", Settings.SelectionFontColor, left, top++);
                Output.Show($"Classification: {o.MenuItem.Classification} || Item Price: {Format.GetMoneyFormat(o.MenuItem.Price)} x {o.Quantity}", left + 4, top++);
                Output.Show($"Total Price: {Format.GetMoneyFormat(o.MenuItem.Price * o.Quantity)}", left + 4, top++);
                row++;
                top++;
            });

            top += 2;
            Output.Show($"Sub Total Cost: {Format.GetMoneyFormat(controller.Order.SubTotalCost)}", left, top++);
            Output.Show($"Inclusive Tax (12%): {Format.GetMoneyFormat(controller.Order.TaxCost)}", left, top++);
            Output.Show($"Service Charge (10%): {Format.GetMoneyFormat(controller.Order.ServiceCharge)}", left, top++);
            Output.Show($"Total Cost: {Format.GetMoneyFormat(controller.Order.TotalCost + controller.Order.ServiceCharge)}", left, top++);

            top = top <= 25 ? 25 : top + 2;
            Border.RenderHorizontalBorder(top);
            Output.Show($"Please hit the ENTER key to continue...", top + 2);
            Border.RenderPageBorder(top + 3);
            Console.SetCursorPosition(0, 0);
        }
    }
}
