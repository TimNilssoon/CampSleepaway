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
                    "Councelor Reports"
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
                        break;
                    case 3:

                        break;
                    default:
                        break;
                }
            }
            throw new NotImplementedException();
        }
    }
}