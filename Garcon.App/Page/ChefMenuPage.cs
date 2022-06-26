using Garcon.App.Page.Component;
using Garcon.App.Controller;
using Garcon.App.App;
using Garcon.App.Page.Abstract;
using Garcon.App.Page.Partials;

namespace Garcon.App.Page
{
    public sealed class ChefMenuPage : ConsolePage
    {
        private readonly OrderController order;
        private readonly MenuController menu;

        public ChefMenuPage(Router p, OrderController order, MenuController menu) : base(p, typeof(ChefMenuPage).Name)
        {
            this.order = order;
            this.menu = menu;
        }

        protected override void ShowContent()
        {
            var list = menu.GetChefRecommendedMenuItems();

            Screen.PrepareConsole();
            MenuList.DisplayMenuItemList("Here is the list of our chef's recommended menu for today:", list);
            Confirm.AddChefRecommendedMenu(router, order, list);
        }
    }
}
