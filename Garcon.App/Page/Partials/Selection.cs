using Garcon.Business.Model;
using Garcon.Business.Enum;
using System.Linq;
using Garcon.App.Styles;
using Garcon.App.Page.Component;
using Garcon.App.Controller;

namespace Garcon.App.Page.Partials
{
    public static class Selection
    {
        public static void ShowHomePageSelection(MenuController menu)
        {
            if (menu.GetAvailableCategorySelectionList().Count == 0)
            {
                Output.Show("Unfortunately, there are no available menu items to order.", 13);
                Output.Show("You may enter any key to refresh the application to reload our content.", 14);
                Border.RenderPageBorder();
                Border.RenderNavbarBorder();
            }
            else
            {
                Output.Show("Please enter a key to navigate the application:", 9);
                int y = 13;

                menu.GetAvailableCategorySelectionList()?.ToList().ForEach(i =>
                {
                    switch (i.Item2)
                    {
                        case nameof(MenuItem.IsChefRecommended):
                            Output.ShowAsColoredSelection($"[{i.Item1}]", "View Chef's Recommended Menu", Settings.SelectionFontColor, 6, y);
                            break;

                        case nameof(MenuItemClassification.Appetizer):
                            Output.ShowAsColoredSelection($"[{i.Item1}]", "View Appetizer items", Settings.SelectionFontColor, 6, y);
                            break;

                        case nameof(MenuItemClassification.MainCourse):
                            Output.ShowAsColoredSelection($"[{i.Item1}]", "View Main Course items", Settings.SelectionFontColor, 6, y);
                            break;

                        case nameof(MenuItemClassification.Dessert):
                            Output.ShowAsColoredSelection($"[{i.Item1}]", "View Dessert items", Settings.SelectionFontColor, 6, y);
                            break;

                        case nameof(MenuItemClassification.Drink):
                            Output.ShowAsColoredSelection($"[{i.Item1}]", "View Drink items", Settings.SelectionFontColor, 6, y);
                            break;
                    }

                    y += 3;
                });

                Border.RenderPageBorder(y);
                Border.RenderNavbarBorder();
            }
        }
    }
}
