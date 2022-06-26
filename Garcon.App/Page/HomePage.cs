using Garcon.Business.Model;
using Garcon.Business.Enum;
using Garcon.App.Page.Component;
using Garcon.App.Styles;
using Garcon.App.Page.Partials;
using Garcon.App.Controller;
using Garcon.App.App;
using Garcon.App.Page.Abstract;


namespace Garcon.App.Page
{
    public sealed class HomePage : ConsolePage
    {
        private readonly OrderController order;
        private readonly MenuController menu;

        public HomePage(Router p, OrderController order, MenuController menu) : base(p, typeof(HomePage).Name)
        {
            this.order = order;
            this.menu = menu;
        }

        protected override void ShowContent()
        {
            Screen.PrepareConsole();
            NavigationContent.HomePage(router, order);
            Selection.ShowHomePageSelection(menu);
            var input = Input.InputLine("Enter Here: ", 4, 3);

            if (int.TryParse(input, out int id))
                switch (menu.GetKeyFromCategorySelectionList(id))
                {
                    // SELECT CHEF RECOMMENDED MENU
                    case nameof(MenuItem.IsChefRecommended):
                        router.SetConsolePage<ChefMenuPage>();
                        break;

                    // SELECT APPETIZER MENU
                    case nameof(MenuItemClassification.Appetizer):
                        router.SetConsolePage<MenuListPage>();
                        order.SelectedMenuClassification = MenuItemClassification.Appetizer;
                        break;

                    // SELECT MAIN COURSE MENU
                    case nameof(MenuItemClassification.MainCourse):
                        router.SetConsolePage<MenuListPage>();
                        order.SelectedMenuClassification = MenuItemClassification.MainCourse;
                        break;

                    // SELECT DESSERT MENU
                    case nameof(MenuItemClassification.Dessert):
                        router.SetConsolePage<MenuListPage>();
                        order.SelectedMenuClassification = MenuItemClassification.Dessert;
                        break;

                    // SELECT DRINK MENU
                    case nameof(MenuItemClassification.Drink):
                        router.SetConsolePage<MenuListPage>();
                        order.SelectedMenuClassification = MenuItemClassification.Drink;
                        break;
                }
            else
                switch (input)
                {
                    // GO TO CART LIST PAGE
                    case "C":
                        router.SetConsolePage<CartListPage>();
                        break;

                    // GO TO PLACE ORDER PAGE
                    case "P":
                        router.SetConsolePage<PlaceOrderPage>();
                        break;

                    // GO TO ORDER LIST PAGE
                    case "O":
                    case "B":
                        router.SetConsolePage<OrderListPage>();
                        break;

                    // GO TO WELCOME PAGE
                    case "X":
                        if (!order.Order.isCancelOrderAllowed())
                        {
                            Prompt.ShowPopUpMessage("You cannot quit because you already have placed an order...", Settings.DangerColor);
                        }
                        else
                        {
                            Confirm.GoToWelcomePage(router);
                        }
                        break;
                }
        }
    }
}
