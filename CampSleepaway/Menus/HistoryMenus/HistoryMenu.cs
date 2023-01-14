using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.HistoryMenus
{
    public class HistoryMenu
    {
        public static void Menu()
        {
            Console.Clear();

            int selection = HelperMethods.ShowMenu("What would you like to do?", new[]
            {
                "List camper history",
                "List next of kin",
                "List councelor history",
                "List cabin history"
            });

            switch (selection)
            {
                case 0:
                    ListCamperHistoryMenu.Menu();
                    break;
                default:
                    break;
            }
        }
    }
}
