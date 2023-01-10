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
                    "Manage Cabins"
                });

                switch (selection)
                {
                    case 0:
                        CampersMenu.Menu();
                        break;
                    default:
                        break;
                }
            }
            throw new NotImplementedException();
        }
    }
}