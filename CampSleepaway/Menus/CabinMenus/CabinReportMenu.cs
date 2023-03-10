using CampSleepaway.Controller;
using CampSleepaway.Data;
using CampSleepaway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.CabinMenus
{
    public class CabinReportMenu
    {
        public static void Menu()
        {
            Console.Clear();

            using CampSleepawayContext context = new();

            // Load all entities into the context.
            // There wont be large enough amounts of data in these tables to warrant more specific calls.
            // EF assigns the objects associated with the cabin automatically through naming conventions/navigational properties
            var campersDb = context.Campers.ToArray();
            var cabinsDb = context.Cabins.ToArray();
            var councelorDb = context.Councelors.ToArray();

            List<string> cabinNames = cabinsDb.Select(c => c.Name).ToList();

            int selection = HelperMethods.ShowMenu("What cabin do you want to check out?", cabinNames);

            ReportWindow(cabinsDb[selection]);
        }

        private static void ReportWindow(Cabin cabin)
        {
            Console.Clear();

            Console.WriteLine($"Cabin: {cabin.Name}");

            if(cabin.Councelor is null)
            {
                Console.WriteLine(" -Warning- Cabin does not have a Councelor");
            }
            else
            {
                Console.WriteLine($"Councelor: {cabin.Councelor.GetFullName()}\n");
            }

            Console.WriteLine("Assigned campers:");

            foreach(var camper in cabin.Campers)
            {
                List<NextOfKin> kins = NextOfKinController.GetAllNextOfKin(camper);
                Console.Write($" - {camper.GetFullName()}");

                if (kins.Count != 0)
                {
                    Console.Write("\t|Relatives: ");
                    foreach (var kin in kins)
                    {
                        Console.Write($"{kin.GetFullName()} ");
                    }
                }
                Console.WriteLine();
            }

            HelperMethods.ShowMessage();
        }
    }
}
