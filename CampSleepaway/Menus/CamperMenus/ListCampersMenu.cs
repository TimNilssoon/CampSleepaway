using CampSleepaway.Controller;
using CampSleepaway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.CamperMenus
{
    public class ListCampersMenu
    {
        public static void Menu()
        {
            Console.Clear();

            List<Camper> campers = CamperController.GetCampers();
            List<string> options = new();

            foreach (var camper in campers)
            {
                options.Add(camper.GetFullName());
            }

            int selection = HelperMethods.ShowMenu("Select a camper to display their information", options);

            DisplayCamperInfo(campers[selection]);
        }

        private static void DisplayCamperInfo(Camper camper)
        {
            Console.Clear();

            string info = camper.GetInfo();

            Console.WriteLine(info);

            HelperMethods.ShowMessage();
        }
    }
}