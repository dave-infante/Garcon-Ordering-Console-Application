using System.Linq;
using Garcon.App.Page.Component;
using Garcon.App.Page.Partials;
using Garcon.App.Controller;
using Garcon.App.App;
using Garcon.App.Page.Abstract;

namespace Garcon.App.Page
{
    public sealed class MenuListPage : ConsolePage
    {
        private readonly OrderController order;
        private readonly MenuController menu;


        public MenuListPage(Router p, OrderController order, MenuController menu) : base(p, typeof(MenuListPage).Name)
        {
            this.order = order;
            this.menu = menu;
        }

        protected override void ShowContent()
        {
            var list = menu.GetByClassification(order.SelectedMenuClassification);

            Screen.PrepareConsole();
            NavigationContent.MenuItemSelection();
            MenuList.DisplayMenuItemList($"Our list of {order.SelectedMenuClassification} items:", list);
            var input = Input.InputLine($"Enter here: ", 4, 3);

            switch (input)
            {
                case "B":
                    router.SetConsolePage<HomePage>();
                    break;

                default:
                    if (int.TryParse(input, out int enteredId))
                    {
                        Confirm.AddItemToCart(router, order, list.ElementAtOrDefault(enteredId - 1));
                    }
                    break;
            }
        }
    }
}
