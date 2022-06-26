using System.Collections.Generic;
using Garcon.Business.Model;
using System;
using Garcon.App.Styles;


namespace Garcon.App.Page.Component
{
    /// <summary>
    /// Generates a reusable component to control the console screen.
    /// </summary>
    public static class Screen
    {
        /// <summary>
        /// Reset the window size from the defined width and height.
        /// </summary>
        public static void ResetWindowSize()
        {
            Console.WindowHeight = Settings.ConsoleHeight;
            Console.WindowWidth = Settings.ConsoleWidth;
        }

        /// <summary>
        /// Prepares the console page content, console window size and provide title bar name.
        /// </summary>
        public static void PrepareConsole(bool willResetWindowSize = false)
        {
            if (willResetWindowSize)
            {
                var affectedOrderItems = new List<OrderItem>();
            }

            Console.Clear();
            Console.Title = Settings.TitleLabel;
            Console.ForegroundColor = Settings.DefaultFontColor;
            Console.BackgroundColor = Settings.BackgroundColor;
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// Clears the content of the navigational bar
        /// </summary>
        public static void ClearNavbarContent()
        {
            for (int CoordsYstart = 1, CoordsYend = 5; CoordsYstart <= CoordsYend; CoordsYstart++)
            {
                for (int CoordsX = 1; CoordsX < Settings.ConsoleWidth - 1; CoordsX++)
                {
                    Output.Show(" ", CoordsX, CoordsYstart);
                }
            }
        }
    }
}
