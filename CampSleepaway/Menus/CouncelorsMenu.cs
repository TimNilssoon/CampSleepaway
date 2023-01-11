using CampSleepaway.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus
{
    public class CouncelorsMenu
    {
        public static void Menu()
        {
            Console.Clear();

            int selection = HelperMethods.ShowMenu("What would you like to do?", new[]
            {
                "List all Councelors",
                "Add Councelor",
                "Edit Councelor"
            });

            switch (selection)
            {
                case 0:
                    ListCampersMenu.Menu();
                    break;
                case 1:
                    CouncelorController.AddCouncelor();
                    break;
                case 2:
                    EditCouncelorsMenu.Menu();
                    break;
                default:
                    break;
            }
        }
    }
}
