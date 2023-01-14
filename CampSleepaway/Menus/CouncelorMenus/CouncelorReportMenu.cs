using CampSleepaway.Data;
using CampSleepaway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.CouncelorMenus
{
    public class CouncelorReportMenu
    {
        public static void Menu()
        {
            Console.Clear();

            using CampSleepawayContext context = new();

            var campersDb = context.Campers.ToArray();
            var cabinsDb = context.Cabins.ToArray();
            var councelorDb = context.Councelors.ToArray();

            List<string> councelorNames = councelorDb.Select(c => c.GetFullName()).ToList();

            int selection = HelperMethods.ShowMenu("What cabin do you want to check out?", councelorNames);

            ReportWindow(councelorDb[selection]);
        }

        private static void ReportWindow(Councelor councelor)
        {
            Console.Clear();

            Console.WriteLine($"Councelor: {councelor.GetFullName()}");
            Console.WriteLine($"Cabin: {councelor.Cabin.Name}\n");
            Console.WriteLine("Assigned campers:");

            foreach (var camper in councelor.Cabin.Campers)
            {
                Console.WriteLine($" - {camper.GetFullName()}");
            }

            HelperMethods.ShowMessage();
        }
    }
}
