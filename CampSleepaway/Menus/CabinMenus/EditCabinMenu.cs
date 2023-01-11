using CampSleepaway.Controller;
using CampSleepaway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.CabinMenus
{
    public class EditCabinMenu
    {
        public static void Menu()
        {
            Console.Clear();

            List<Cabin> cabins = CabinController.GetCabins();
            List<string> options = new();

            foreach (var cabin in cabins)
            {
                options.Add(cabin.Name);
            }

            int selection = HelperMethods.ShowMenu("Which cabin would you like to rename?", options);

            CabinController.UpdateCabinName(cabins[selection].CabinId);
        }
    }
}
