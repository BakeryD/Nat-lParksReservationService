using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class CLI
    {
        /// <summary>
        /// Generates a menu and returns the user choice
        /// </summary>
        /// <param name="menuOptions">A list of strings compirsing the Menu options</param>
        /// <param name="allowQuit">Whther to allow the user to quit the menu (returns "Q")</param>
        /// <returns>A string containing the 1 indexed number corresponding to the menu option</returns>
    }
    public static string GenerateAMenu(List<string> menuOptions, bool allowQuit)
    {
        HashSet<string> validChoices = new HashSet<string>();
        string input = "";

        do
        {
            Console.WriteLine();
            for (int i = 1; i <= menuOptions.Count; i++)
            {
                validChoices.Add(i.ToString());
                Console.WriteLine($"{i}. {menuOptions[i - 1]}");
            }
            if (allowQuit)
            {
                validChoices.Add("Q");
                Console.Write("Q. Quit");
            }
            Console.Write("Pick an option: ");
            input = Console.ReadLine().ToUpper();
            Console.Clear();
        } while (!validChoices.Contains(input));

        return input;
    }

}
