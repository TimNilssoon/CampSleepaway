using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampSleepaway.Controller;
using CampSleepaway.Menus.CamperMenus;

namespace CampSleepaway.Menus
{
    public class CampersMenu
    {
        public static void Menu()
        {
            Console.Clear();

            int selection = HelperMethods.ShowMenu("What would you like to do?", new[]
            {
                "List all campers",
                "Add Camper",
                "Edit camper"
            });

            switch (selection)
            {
                case 0:
                    ListCampersMenu.Menu();
                    break;
                case 1:
                    CamperController.AddCamper();
                    break;
                case 2:
                    EditCamperMenu.Menu();
                    break;
                default:
                    break;
            }
        }
    }
}
