using Garcon.App.Page.Component;
using Garcon.App.Styles;
using Garcon.App.Page.Partials;
using Garcon.App.Controller;
using Garcon.App.App;
using Garcon.App.Page.Abstract;

namespace Garcon.App.Page
{
    public sealed class OrderListPage : ConsolePage
    {
        private readonly OrderController order;

        public OrderListPage(Router p, OrderController order) : base(p, typeof(OrderListPage).Name)
            => this.order = order;

        public override void ShowPage()
        {
            if (order.Order.GetOrderedItems().Count == 0)
            {
                router.SetConsolePage<HomePage>();
                Prompt.ShowPopUpMessage("Your order list is empty. Please place an order item first and try again.", Settings.DangerColor);
            }
            else
            {
                while (router.IsCurrentPage(pageName))
                {
                    ShowContent();
                }
            }
        }

        protected override void ShowContent()
        {
            var list = order.Order.GetOrderedItems();

            Screen.PrepareConsole();
            NavigationContent.ManageOrderItem();
            MenuList.DisplayProcessingItemList("Here is the list of items you have ordered:", list);

            switch (Input.InputLine("Enter Here: ", 4, 3))
            {
                case "B":
                    if (!order.Order.isBillOutAllowed())
                    {
                        Prompt.ShowPopUpMessage("Bill out is not allowed. Please wait until all the ordered items have been served.", Settings.DangerColor);
                    }
                    else
                    {
                        Confirm.Billout(router, order);
                    }
                    break;

                case "R":
                    list = order.Order.GetOrderedItems();
                    break;

                case "G":
                    router.SetConsolePage<HomePage>();
                    break;
            }
        }
    }
}
