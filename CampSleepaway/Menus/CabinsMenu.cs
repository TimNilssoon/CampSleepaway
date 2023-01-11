using CampSleepaway.Controller;
using CampSleepaway.Menus.CamperMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus
{
    public class CabinsMenu
    {
        public static void Menu()
        {
            Console.Clear();

            int selection = HelperMethods.ShowMenu("What would you like to do?", new[]
            {
                "Add Cabin",
                "Edit Cabin"
            });

            switch (selection)
            {
                case 0:
                    CabinController.AddCabin();
                    break;
                case 1:
                    EditCamperMenu.Menu();
                    break;
                default:
                    break;
            }
        }
    }
}
