using Garcon.App.Styles;
using System;


namespace Garcon.App.Page.Component
{
    public static class Output
    {
        /// <summary>
        /// Render output to a specified coordinates.
        /// </summary>
        public static void Show(string Output, int CoordsX = 0, int CoordsY = 0)
        {
            Console.SetCursorPosition(Math.Abs(CoordsX), Math.Abs(CoordsY));
            Console.Write(Output);
        }

        /// <summary>
        /// Render output that is centered to the x axis.
        /// </summary>
        public static void Show(string Output, int CoordsY = 0)
        {
            int CenterCoordsx = (Settings.ConsoleWidth - 2) / 2 - Output.Length / 2;
            Show(Output, CenterCoordsx, CoordsY);
        }

        /// <summary>
        /// Render output to a specified coordinates with font color
        /// </summary>
        public static void Show(string Output, ConsoleColor color, int CoordsX = 0, int CoordsY = 0)
        {
            Console.ForegroundColor = color;
            Show(Output, CoordsX, CoordsY);
            Console.ForegroundColor = Settings.DefaultFontColor;
        }

        /// <summary>
        /// Render message output that is centered to the x axis with font color
        /// </summary>
        public static void Show(string Output, ConsoleColor color, int CoordsY = 0)
        {
            Console.ForegroundColor = color;
            Show(Output, CoordsY);
            Console.ForegroundColor = Settings.DefaultFontColor;
        }

        /// <summary>
        /// Generates a bullet style list indicating a colored key with its definition.
        /// </summary>
        public static void ShowAsColoredSelection(string SelectionKey, string SelectionDetails, ConsoleColor fontColor, int CoordsX, int CoordsY)
        {
            Show(SelectionKey, fontColor, CoordsX, CoordsY);
            Show(" - " + SelectionDetails, CoordsX + SelectionKey.Length, CoordsY);
        }
    }
}
