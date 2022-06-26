using Garcon.App.Styles;
using Garcon.App.Controller;
using Garcon.App.App;
using Garcon.App.Page.Component;

namespace Garcon.App.Page.Partials
{
    public static class NavigationContent
    {
        public static void HomePage(Router handler, OrderController controller)
        {
            Output.Show("Enter the following key to go to page:", 33, 1);
            Output.ShowAsColoredSelection("[P]", $"Place Order", Settings.NavigationFontColor, 33, 3);
            Output.ShowAsColoredSelection("[C]", $"View Cart Items ({controller.Order.GetTotalCartItemQuantity()})", Settings.NavigationFontColor, 33, 4);
            Output.ShowAsColoredSelection("[O]", $"View Ordered Items ({controller.Order.GetTotalOrderItemQuantity()})", Settings.NavigationFontColor, 33, 5);
            Output.ShowAsColoredSelection("[B]", "Proceed to Bill Out", Settings.NavigationFontColor, 66, 3);
            Output.ShowAsColoredSelection("[X]", "Quit", Settings.NavigationFontColor, 66, 4);
        }
        public static void MenuItemSelection()
        {
            Output.ShowAsColoredSelection("[#]", "Type the menu ID you want to add", Settings.NavigationFontColor, 33, 2);
            Output.ShowAsColoredSelection("[B]", "Go back to home page", Settings.NavigationFontColor, 33, 4);
        }

        public static void ManageCartList()
        {
            Output.ShowAsColoredSelection("[#]", "Enter Menu ID to Select", Settings.NavigationFontColor, 33, 2);
            Output.ShowAsColoredSelection("[P]", "Place Order", Settings.NavigationFontColor, 33, 4);
            Output.ShowAsColoredSelection("[C]", "Clear your Cart", Settings.NavigationFontColor, 66, 2);
            Output.ShowAsColoredSelection("[B]", "Go back to Home Page", Settings.NavigationFontColor, 66, 4);
        }

        public static void ManageCartItem()
        {
            Output.ShowAsColoredSelection("[A]", "Add Quantity", Settings.NavigationFontColor, 33, 2);
            Output.ShowAsColoredSelection("[S]", "Subract Quantity", Settings.NavigationFontColor, 33, 4);
            Output.ShowAsColoredSelection("[R]", "Remove Order item", Settings.NavigationFontColor, 60, 2);
            Output.ShowAsColoredSelection("[B]", "Go back to Cart", Settings.NavigationFontColor, 60, 4);
        }

        public static void ManageOrderItem()
        {
            Output.ShowAsColoredSelection("[B]", "Bill-Out Order", Settings.NavigationFontColor, 33, 2);
            Output.ShowAsColoredSelection("[R]", "Refresh List", Settings.NavigationFontColor, 33, 4);
            Output.ShowAsColoredSelection("[G]", "Go back to Home Page", Settings.NavigationFontColor, 66, 2);
        }

        public static void PlaceOrderPage()
        {
            Output.ShowAsColoredSelection("[P]", "Confirm Place Order", Settings.NavigationFontColor, 33, 2);
            Output.ShowAsColoredSelection("[C]", "Cancel Order", Settings.NavigationFontColor, 33, 4);
            Output.ShowAsColoredSelection("[B]", "Go back to Home Page", Settings.NavigationFontColor, 66, 2);
        }

        public static void CartItemsWithIngredientDeficiency()
        {
            Output.ShowAsColoredSelection("[#]", "Enter Menu ID you want to keep as your order", Settings.NavigationFontColor, 33, 2);
            Output.ShowAsColoredSelection("[N]", "None of the selection", Settings.NavigationFontColor, 33, 4);
            Output.ShowAsColoredSelection("[B]", "Abort Placing Order", Settings.NavigationFontColor, 66, 4);
        }
    }
}
