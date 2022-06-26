using Garcon.App.Styles;
using System;


namespace Garcon.App.Page.Component
{
    public static class Prompt
    {
        /// <summary>
        /// Shows a pop up message to emphasize a response.
        /// </summary>
        public static void ShowPopUpMessage(string text, ConsoleColor color)
        {
            Screen.PrepareConsole();
            Border.RenderPageBorder();

            Output.Show(text, color, Settings.ConsoleHeight / 2 - 3);
            Output.Show("Please enter any key to proceed...", Settings.ConsoleHeight / 2 - 2);
            Input.InputKey("", Settings.ConsoleWidth / 2, Settings.ConsoleHeight / 2);
        }

        /// <summary>
        /// Shows a pop up message to emphasize a response.
        /// </summary>
        public static void ShowPopUpMessage(string text1, string text2, ConsoleColor color)
        {
            Screen.PrepareConsole();
            Border.RenderPageBorder();

            Output.Show(text1, color, Settings.ConsoleHeight / 2 - 3);
            Output.Show(text2, color, Settings.ConsoleHeight / 2 - 2);
            Output.Show("Please enter any key to proceed...", Settings.ConsoleHeight / 2);
            Input.InputKey("", Settings.ConsoleWidth / 2, Settings.ConsoleHeight / 2 + 3);
        }

        /// <summary>
        /// Display a closing message.
        /// </summary>
        public static void ShowCloseMessage()
        {
            Screen.PrepareConsole();
            Border.RenderPageBorder();

            Output.Show("Application will now close", 5);
            Output.Show("Please enter any key to proceed..", 6);
            Input.InputKey("", Settings.ConsoleWidth / 2, 10);
            Screen.PrepareConsole();
        }

        /// <summary>
        /// Clears the screen to show the error screen.
        /// </summary>
        public static void ShowErrorMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = Settings.DangerColor;
            Console.WriteLine("An error has occured in the application with error message: ");
            Console.WriteLine(message);

            Console.ForegroundColor = Settings.DefaultFontColor;
            Console.WriteLine("\nPlease enter any key to reset the app...");
            Console.Read();
        }
    }
}
