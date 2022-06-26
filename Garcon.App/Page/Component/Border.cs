using Garcon.App.Styles;

namespace Garcon.App.Page.Component
{
    /// <summary>
    /// Handles the border design
    /// </summary>
    public static class Border
    {
        /// <summary>
        /// Generate a border for the navigational bar on top of the main screen.
        /// </summary>
        public static void RenderNavbarBorder()
        {
            for (int i = 0; i < Settings.ConsoleWidth; i++) Output.Show(Settings.BorderSymbol.ToString(), Settings.BorderColor, i, 6);
            for (int i = 0; i < 7; i++) Output.Show(Settings.BorderSymbol.ToString(), Settings.BorderColor, 30, i);
        }

        /// <summary>
        /// Render screen border around the window screen.
        /// </summary>
        public static void RenderPageBorder(int YCoords = 28)
        {
            for (int row = 0, extendedHeight = YCoords < 28 ? 30 : YCoords + 2; row < extendedHeight; row++)
                for (int column = 0; column < Settings.ConsoleWidth; column++)
                    if (row == 0 || row == extendedHeight - 1 || column == 0 || column == Settings.ConsoleWidth - 1)
                        Output.Show(Settings.BorderSymbol.ToString(), Settings.BorderColor, column, row);
        }

        /// <summary>
        /// Generate a horizontal border.
        /// </summary>
        public static void RenderHorizontalBorder(int YCoords)
        {
            for (int column = 0; column < Settings.ConsoleWidth; column++)
                Output.Show(Settings.BorderSymbol.ToString(), Settings.BorderColor, column, YCoords);
        }
    }
}
