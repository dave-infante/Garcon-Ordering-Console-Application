using System.Collections.Generic;
using Garcon.App.Page;
using System.Linq;
using System;
using Garcon.App.Page.Abstract;
using Garcon.App.Page.Component;

namespace Garcon.App.App
{
    public sealed class Router
    {
        private List<ConsolePage> _pageList = new List<ConsolePage>();
        private bool _isPageRouteRunning = false;
        private string _currentPage = "";

        public Router(GarconApp app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app), "Argument cannot be null.");
            }

            RegisterPages(app);
        }

        /// <summary>
        /// Registers console pages that will be used (e.g. Welcome page, order page, payment page)
        /// </summary>
        private void RegisterPages(GarconApp app)
        {
            _pageList.Add(new WelcomePage(this, app.orderController));
            _pageList.Add(new CartListPage(this, app.orderController));
            _pageList.Add(new PlaceOrderPage(this, app.orderController));
            _pageList.Add(new OrderListPage(this, app.orderController));
            _pageList.Add(new HomePage(this, app.orderController, app.menuController));
            _pageList.Add(new MenuListPage(this, app.orderController, app.menuController));
            _pageList.Add(new ChefMenuPage(this, app.orderController, app.menuController));
            _pageList.Add(new ShortageSupplyPage(this, app.orderController, app.menuController));
        }


        /// <summary>
        /// Handles routing by traversing to the registered console page.
        /// </summary>
        public void Initialize()
        {
            if (_isPageRouteRunning)
            {
                throw new InvalidOperationException("Page handler is already running. Multiple execution is not allowed.");
            }

            SetConsolePage<WelcomePage>();
            _isPageRouteRunning = true;

            while (_isPageRouteRunning)
            {
                try
                {
                    _pageList.FirstOrDefault(p => IsCurrentPage(p.pageName)).ShowPage();
                }
                catch (Exception e)
                {
                    Prompt.ShowErrorMessage(e.Message);
                    SetConsolePage<WelcomePage>();
                }
            }

            Prompt.ShowCloseMessage();
        }


        /// <summary>
        /// Checks if the provided page name is the current console page.
        /// </summary>
        public bool IsCurrentPage(string PageName)
            => _currentPage == PageName;


        /// <summary>
        /// Route to the designated console page.
        /// </summary>
        public void SetConsolePage<T>() where T : ConsolePage
        {
            if (!_pageList.Any(p => p.pageName == typeof(T).Name))
            {
                throw new ArgumentException($"Console page {typeof(T).Name} does not exist in the registered page list. Please consider registering the said console page first and try again.");
            }

            _currentPage = typeof(T).Name;
        }
    }
}
