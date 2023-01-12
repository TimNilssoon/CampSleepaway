using CampSleepaway.Menus.CabinMenus;
using CampSleepaway.Menus.CouncelorMenus;
using CampSleepaway.Menus.VistorMenus;

namespace CampSleepaway.Menus
{
    public class MainMenu
    {
        public static void Menu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("Main Menu");
                Console.WriteLine("---------");

                int selection = HelperMethods.ShowMenu("What would you like to do?", new string[]
                {
                    "Manage Campers",
                    "Manage Councelors",
                    "Manage Cabins",
                    "Cabin Reports",
                    "Councelor Reports",
                    "Visitor Information"
                });

                switch (selection)
                {
                    case 0:
                        CampersMenu.Menu();
                        break;
                    case 1:
                        CouncelorsMenu.Menu();
                        break;
                    case 2:
                        CabinsMenu.Menu();
                        break;
                    case 3:
                        CabinReportMenu.Menu();
                        break;
                    case 4:
                        CouncelorReportMenu.Menu();
                        break;
                    case 5:
                        VisitorInfoMenu.Menu();
                        break;
                    default:
                        break;
                }
            }
            throw new NotImplementedException();
        }
    }
}