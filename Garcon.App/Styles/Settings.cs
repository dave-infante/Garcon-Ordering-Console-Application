using System;

namespace Garcon.App.Styles
{
    public static class Settings
    {
        // TITLE BAR LABEL
        public static readonly string TitleLabel = "Garçon Ordering Application";

        // WINDOW DIMENSION
        public static readonly int ConsoleWidth = 100;
        public static readonly int ConsoleHeight = 30;

        // FONT COLOR
        public static readonly ConsoleColor NavigationFontColor = ConsoleColor.Green;
        public static readonly ConsoleColor SelectionFontColor = ConsoleColor.Yellow;
        public static readonly ConsoleColor DefaultFontColor = ConsoleColor.White;
        public static readonly ConsoleColor WelcomeFontColor = ConsoleColor.DarkYellow;

        // BACKGROUND COLOR
        public static readonly ConsoleColor BackgroundColor = ConsoleColor.Black;

        // FEEDBACK COLOR
        public static readonly ConsoleColor DangerColor = ConsoleColor.Red;
        public static readonly ConsoleColor WarningColor = ConsoleColor.Yellow;
        public static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
        public static readonly ConsoleColor InfoColor = ConsoleColor.Blue;

        // BORDER SETTINGS
        public static readonly ConsoleColor BorderColor = ConsoleColor.DarkRed;
        public static readonly char BorderSymbol = 'X';

        // INPUT SETTINGS
        public static readonly ConsoleColor InputFontColor = ConsoleColor.Red;

    }
}
