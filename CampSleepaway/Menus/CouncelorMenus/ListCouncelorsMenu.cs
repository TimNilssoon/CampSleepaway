using CampSleepaway.Controller;
using CampSleepaway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.CouncelorMenus
{
    public class ListCouncelorsMenu
    {
        public static void Menu()
        {
            Console.Clear();

            List<Councelor> councelors = CouncelorController.GetCouncelors();
            List<string> options = new();

            foreach (var councelor in councelors)
            {
                options.Add(councelor.GetFullName());
            }

            int selection = HelperMethods.ShowMenu("Select a camper to display their information", options);

            DisplayCouncelorInfo(councelors[selection]);
        }
        private static void DisplayCouncelorInfo(Councelor councelor)
        {
            Console.Clear();

            string info = councelor.GetInfo();

            Console.WriteLine(info);

            HelperMethods.ShowMessage();
        }
    }
}
