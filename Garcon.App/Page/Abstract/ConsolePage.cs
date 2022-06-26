using Garcon.App.App;
using System;


namespace Garcon.App.Page.Abstract
{
    public abstract class ConsolePage
    {
        protected readonly Router router;
        public readonly string pageName;

        /// <summary>
        /// Method that represents the UI view of a given page
        /// </summary>
        protected abstract void ShowContent();

        public ConsolePage(Router router, string pageName)
        {
            if (router is null)
            {
                throw new ArgumentNullException("Router cannot be null.");
            }

            if (string.IsNullOrEmpty(pageName))
            {
                throw new ArgumentException("PageName cannot be empty.");
            }

            this.router = router;
            this.pageName = pageName;
        }


        /// <summary>
        /// Temporarily locks the user in current console page view
        /// </summary>
        public virtual void ShowPage()
        {
            while (router.IsCurrentPage(pageName))
            {
                ShowContent();
            }
        }
    }
}
