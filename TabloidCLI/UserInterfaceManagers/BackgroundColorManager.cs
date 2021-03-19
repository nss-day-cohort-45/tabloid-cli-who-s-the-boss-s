using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.UserInterfaceManagers
{
    class BackgroundColorManager : IUserInterfaceManager
    {
        public IUserInterfaceManager Execute()
        {
            // Need to show list of available colors here

            //  Get an array with the values of ConsoleColor enumeration members
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

            // Save the current colors in the background and foreground
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            // Display all background colors except the one that matches the foreground
            // (We want to make sure the text on the screen is visible!)
            Console.WriteLine($"All the background colors except {currentForeground}, the foreground color");

            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[0] == currentForeground) continue;

                Console.WriteLine($"{i + 1} {colors[i]}");
            }

            Console.Write("> ");
            string input = Console.ReadLine();
            try
            {
                int choice = (int.Parse(input) - 1);
                ConsoleColor colorChoice = colors[choice];
                Console.BackgroundColor = colorChoice;
                // Do we return something, or just set it here?
                return new MainMenuManager();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                // Do we return 'this' to re-run the color choice process?
                return this;
            }

        }
    }
}
