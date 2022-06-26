using Garcon.App.Styles;
using System;


namespace Garcon.App.Page.Component
{
    /// <summary>
    /// Generates input components 
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// Create a component that accepts an input key with placeholder.
        /// </summary>
        public static ConsoleKey InputKey(string Placeholder, int CoordsX, int CoordsY)
        {
            Console.SetCursorPosition(0, 0);
            Output.Show(Placeholder + " ", CoordsX, CoordsY);

            Console.ForegroundColor = Settings.InputFontColor;
            var input = Console.ReadKey().Key;
            Console.ForegroundColor = Settings.DefaultFontColor;
            return input;
        }

        /// <summary>
        /// Create a component that accepts an input line with placeholder.
        /// </summary>
        public static string InputLine(string Placeholder, int CoordsX, int CoordsY)
        {
            Console.SetCursorPosition(0, 0);
            Output.Show(Placeholder, CoordsX, CoordsY);
            Console.SetCursorPosition(CoordsX + Placeholder.Length, CoordsY);

            Console.ForegroundColor = Settings.InputFontColor;
            var input = Console.ReadLine().Trim().ToUpper();
            Console.ForegroundColor = Settings.DefaultFontColor;
            return input;
        }

        /// <summary>
        /// Generates a confirmation input that accepts two values (Y/N).
        /// </summary>
        public static bool ShowConfirmation(string question)
        {
            string query = "Enter here ";
            string selection = "[Y/N]";

            while (true)
            {
                Screen.ClearNavbarContent();
                Border.RenderNavbarBorder();

                Output.Show(query, 4, 3);
                Output.Show(selection, Settings.NavigationFontColor, query.Length + 4, 3);
                Output.Show(question, 33, 3);

                switch (InputLine(": ", selection.Length + query.Length + 4, 3))
                {
                    case "Y": return true;
                    case "N": return false;
                }
            }
        }

        /// <summary>
        /// Generates a confirmation input that accepts two values (Y/N).
        /// </summary>
        public static bool ShowConfirmation(string question, string subInfo)
        {
            string query = "Enter here ";
            string selection = "[Y/N]";

            while (true)
            {
                Screen.ClearNavbarContent();
                Border.RenderNavbarBorder();

                Output.Show(query, 4, 3);
                Output.Show(selection, Settings.NavigationFontColor, query.Length + 4, 3);
                Output.Show(question, 33, 2);
                Output.Show(subInfo, 33, 4);

                switch (InputLine(": ", selection.Length + query.Length + 4, 3))
                {
                    case "Y": return true;
                    case "N": return false;
                }
            }
        }
    }
}
