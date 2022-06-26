using System;
using Garcon.App.Controller;
using Garcon.App.App;
using Garcon.App.Page.Partials;
using Garcon.App.Page.Abstract;
using Garcon.App.Page.Component;

namespace Garcon.App.Page
{
    public sealed class WelcomePage : ConsolePage
    {
        private readonly OrderController order;

        public WelcomePage(Router p, OrderController order) : base(p, typeof(WelcomePage).Name)
            => this.order = order;

        protected override void ShowContent()
        {
            Screen.PrepareConsole(true);
            Welcome.ShowGreetings();

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Enter:
                    router.SetConsolePage<HomePage>();
                    order.CreateNewOrder();
                    break;
            }
        }
    }
}
