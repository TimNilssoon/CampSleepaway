using CampSleepaway.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway
{
    public class HelperMethods
    {
        public static int ShowMenu(string prompt, IEnumerable<string> options)
        {
            if (options == null || options.Count() == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty list of options.");
            }

            Console.WriteLine(prompt);

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            // Calculate the width of the widest option so we can make them all the same width later.
            int width = options.Max(option => option.Length);

            int selected = 0;
            int top = Console.CursorTop;
            for (int i = 0; i < options.Count(); i++)
            {
                // Start by highlighting the first option.
                if (i == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var option = options.ElementAt(i);
                // Pad every option to make them the same width, so the highlight is equally wide everywhere.
                Console.WriteLine("- " + option.PadRight(width));

                Console.ResetColor();
            }
            Console.CursorLeft = 0;
            Console.CursorTop = top - 1;

            var rnd = new Random();

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(intercept: true).Key;

                // First restore the previously selected option so it's not highlighted anymore.
                Console.CursorTop = top + selected;
                string oldOption = options.ElementAt(selected);
                Console.Write("- " + oldOption.PadRight(width));
                Console.CursorLeft = 0;
                Console.ResetColor();

                // Then find the new selected option.
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Count() - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }

                // Finally highlight the new selected option.
                Console.CursorTop = top + selected;
                Console.BackgroundColor = (ConsoleColor)rnd.Next(1, 14);
                //Console.Beep();
                Console.ForegroundColor = ConsoleColor.White;
                string newOption = options.ElementAt(selected);
                Console.Write("- " + newOption.PadRight(width));
                Console.CursorLeft = 0;
                // Place the cursor one step above the new selected option so that we can scroll and also see the option above.
                Console.CursorTop = top + selected - 1;
                Console.ResetColor();
            }

            // Afterwards, place the cursor below the menu so we can see whatever comes next.
            Console.CursorTop = top + options.Count();

            // Show the cursor again and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }

        public static string GetString(string prompt)
        {
            Console.Clear();
            Console.Write($"{prompt} ");

            return Console.ReadLine();
        }

        public static void ShowMessage()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static void ShowMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static DateTime GetDateTime(string prompt)
        {
            Console.Clear();
            Console.Write($"{prompt} ");

            DateTime result;

            while (!DateTime.TryParse(Console.ReadLine(), out result))
            {
                Console.Clear();
                Console.WriteLine("Invalid input, try again...");

                Console.Write($"{prompt} ");
            }

            return result;
        }

        public static bool GetBool(string prompt)
        {
            Console.Clear();
            Console.Write($"{prompt} ");

            bool result;
            while (!bool.TryParse(Console.ReadLine(), out result))
            {
                Console.Clear();
                Console.WriteLine("Invalid input, try again...");

                Console.Write($"{prompt} ");
            }

            return result;
        }

        public static int GetInt(string prompt)
        {
            Console.Clear();
            Console.Write(prompt);

            int newFavorite;

            while (!int.TryParse(Console.ReadLine(), out newFavorite))
            {
                Console.Write("Please enter a number: ");
            }

            return newFavorite;
        }

        public static RelationType GetRelationType(string prompt)
        {
            List<string> types = Enum.GetNames(typeof(RelationType)).ToList();

            int selection = ShowMenu(prompt, types);

            return (RelationType)selection;
        }
    }
}
