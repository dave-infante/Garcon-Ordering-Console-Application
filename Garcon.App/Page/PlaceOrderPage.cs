using Garcon.App.Page.Component;
using Garcon.App.Styles;
using Garcon.App.Page.Partials;
using Garcon.App.Controller;
using Garcon.App.App;
using Garcon.App.Page.Abstract;


namespace Garcon.App.Page
{
    public sealed class PlaceOrderPage : ConsolePage
    {
        private readonly OrderController order;

        public PlaceOrderPage(Router p, OrderController order) : base(p, typeof(PlaceOrderPage).Name)
            => this.order = order;

        public override void ShowPage()
        {
            if (!order.Order.isPlaceOrderAllowed())
            {
                router.SetConsolePage<HomePage>();
                Prompt.ShowPopUpMessage("Place order is not allowed since your cart list is currently empty. ", Settings.DangerColor);
            }
            else
            {
                Prompt.ShowPopUpMessage("Initializing order placement...", Settings.InfoColor);
                while (router.IsCurrentPage(pageName)) ShowContent();
            }
        }

        protected override void ShowContent()
        {
            Screen.PrepareConsole();
            NavigationContent.PlaceOrderPage();
            MenuList.DisplayOrderItemList("Here is the list of order items currently in your cart:", order.Order.GetCartItems());

            switch (Input.InputLine($"Enter here: ", 4, 3))
            {
                case "P":
                    Confirm.PlaceOrder(router, order);
                    break;

                case "C":
                    if (!order.Order.isCancelOrderAllowed())
                        Prompt.ShowPopUpMessage("Order cancellation is not allowed since there is an on-going processing of order items.", Settings.DangerColor);
                    else
                        Confirm.CancelOrder(router, order);
                    break;

                case "B":
                    router.SetConsolePage<HomePage>();
                    break;
            }
        }
    }
}
