using System.Collections.Generic;
using Garcon.Business.Model;
using System.Linq;
using Garcon.App.Page.Component;
using Garcon.App.Styles;
using Garcon.App.Page.Partials;
using Garcon.App.Controller;
using Garcon.App.App;
using Garcon.App.Page.Abstract;


namespace Garcon.App.Page
{
    public sealed class CartListPage : ConsolePage
    {
        private readonly OrderController order;

        public CartListPage(Router p, OrderController order) : base(p, typeof(CartListPage).Name)
        {
            this.order = order;
        }

        public override void ShowPage()
        {
            if (order.Order.GetCartItems().Count == 0)
            {
                router.SetConsolePage<HomePage>();
                Prompt.ShowPopUpMessage("Your cart list is currently empty. Please add a menu item and try again.", Settings.DangerColor);
            }
            else
            {
                do
                {
                    if (order.Order.GetCartItems().Count == 0)
                    {
                        router.SetConsolePage<HomePage>();
                    }
                    else
                    {
                        ShowContent();
                    }
                }
                while (router.IsCurrentPage(pageName));
            }
        }

        protected override void ShowContent()
        {
            Screen.PrepareConsole();
            NavigationContent.ManageCartList();
            MenuList.DisplayOrderItemList("Your list of orders currently in cart:", order.Order.GetCartItems());

            var input = Input.InputLine($"Enter here: ", 4, 3);
            switch (input)
            {
                case "P":
                    if (order.Order.GetCartItems().Count == 0)
                    {
                        Prompt.ShowPopUpMessage("Your cart list is currently empty. Please add a menu item and try again.", Settings.DangerColor);
                    }
                    else
                    {
                        router.SetConsolePage<PlaceOrderPage>();
                    }
                    break;

                case "B":
                    router.SetConsolePage<HomePage>();
                    break;

                case "C":
                    Confirm.ClearCart(order);
                    break;

                default:
                    if (int.TryParse(input, out int enteredId))
                    {
                        var selectedItem = order.Order.GetCartItems().ElementAtOrDefault(enteredId - 1);
                        SelectOrderItem(selectedItem);
                    }
                    break;
            }
        }

        private void SelectOrderItem(OrderItem orderItem)
        {
            while (!(orderItem is null))
            {
                Screen.PrepareConsole();
                NavigationContent.ManageCartItem();
                MenuList.DisplayOrderItemList("Selected Order Item:", new List<OrderItem>() { orderItem });

                switch (Input.InputLine($"Enter here: ", 4, 3))
                {
                    case "A":
                        order.Order.AddItemToCart(orderItem.MenuItem);
                        break;

                    case "S":
                        if (orderItem.Quantity > 1)
                        {
                            order.Order.SubtractItemFromCart(orderItem.MenuItem);
                        }
                        else
                        {
                            Confirm.RemoveItemFromCart(order, orderItem);
                            return;
                        }
                        break;

                    case "R":
                        Confirm.RemoveItemFromCart(order, orderItem);
                        return;

                    case "B":
                        return;
                }
            }
        }
    }
}
