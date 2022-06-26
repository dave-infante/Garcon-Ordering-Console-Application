using Garcon.App.Page.Component;
using Garcon.App.Styles;

namespace Garcon.App.Page.Partials
{
    public static class Welcome
    {
        public static void ShowGreetings()
        {
            var y = 2;

            Output.Show("        ___,                                                                                 ", Settings.WelcomeFontColor, y++);
            Output.Show("       '._.'\\             /              ¶                   ¶                   ¶          ", Settings.WelcomeFontColor, y++);
            Output.Show("    _____/'-.\\    ______ /              ¶¶¶                 ¶¶¶                 ¶¶¶         ", Settings.WelcomeFontColor, y++);
            Output.Show("   |    / |      \\------//             ¶¶¶¶¶               ¶¶¶¶¶               ¶¶¶¶¶        ", Settings.WelcomeFontColor, y++);
            Output.Show("   |~~~/~~|       \\   _//        ¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶   ¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶   ¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶  ", Settings.WelcomeFontColor, y++);
            Output.Show("  \\ ()   /        \\ (_/            ¶¶¶¶¶¶¶¶¶¶¶         ¶¶¶¶¶¶¶¶¶¶¶         ¶¶¶¶¶¶¶¶¶¶¶     ", Settings.WelcomeFontColor, y++);
            Output.Show("    '.__.'          \\_/               ¶¶¶¶¶¶¶             ¶¶¶¶¶¶¶             ¶¶¶¶¶¶¶       ", Settings.WelcomeFontColor, y++);
            Output.Show("      ||             |               ¶¶¶¶ ¶¶¶¶           ¶¶¶¶ ¶¶¶¶           ¶¶¶¶ ¶¶¶¶       ", Settings.WelcomeFontColor, y++);
            Output.Show("     _||_            |              ¶¶¶     ¶¶¶         ¶¶¶     ¶¶¶         ¶¶¶     ¶¶¶      ", Settings.WelcomeFontColor, y++);
            Output.Show("    `----`         __|__           ¶¶         ¶¶       ¶¶         ¶¶       ¶¶         ¶¶     ", Settings.WelcomeFontColor, y++);
            Output.Show("                                                                                             ", Settings.WelcomeFontColor, y++);
            Output.Show("                                                                                             ", Settings.WelcomeFontColor, y++);
            Output.Show("          ,adPPYb,d8  ,adPPYYba,  8b,dPPYba,   ,adPPYba,   ,adPPYba,   8b,dPPYba,            ", Settings.WelcomeFontColor, y++);
            Output.Show("         a8'    `Y88  ''     `Y8  88P'   'Y8  a8'     ''  a8'     '8a  88P'   `'8a           ", Settings.WelcomeFontColor, y++);
            Output.Show("         8b       88  ,adPPPPP88  88          8b          8b       d8  88       88           ", Settings.WelcomeFontColor, y++);
            Output.Show("         '8a,   , d8  88,    ,88  88          '8a,   ,aa  '8a,   , a8  88       88           ", Settings.WelcomeFontColor, y++);
            Output.Show("          `'YbbdP'Y8  `'8bbdP'Y8  88           `'Ybbd8''   `'YbbdP''   88       88           ", Settings.WelcomeFontColor, y++);
            Output.Show("          aa,    ,88                              8'                                         ", Settings.WelcomeFontColor, y++);
            Output.Show("           'Y8bbdP'                                                                          ", Settings.WelcomeFontColor, y++);

            Border.RenderHorizontalBorder(25);
            Border.RenderPageBorder();

            Output.Show($"Welcome to Mang Simang's Bistro!", 22);
            Output.Show($"You can call me Garçon. I am here to assist you with your orders.", 23);
            Output.Show("HIT THE ENTER KEY TO PROCEED ", 27);
        }
    }
}

