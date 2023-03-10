using CampSleepaway.Controller;
using CampSleepaway.Data;
using CampSleepaway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.VistorMenus
{
    public class VisitorInfoMenu
    {
        public static void Menu()
        {
            Console.Clear();

            Console.WriteLine("Visitor Menu");
            Console.WriteLine("------------");

            // Gets all campers to select which camper to visit

            List<Camper> campers = CamperController.GetCampers();
            List<string> names = campers.Select(c => c.GetFullName()).ToList();

            int selection = HelperMethods.ShowMenu("Who are you visiting?", names);

            Camper camper = campers[selection];


            // Checks if there is a visit on the selected camper
            using CampSleepawayContext context = new();

            Visit visit = context.Visits.SingleOrDefault(c => c.CamperId == camper.CamperId);

            if (visit is null)
            {
                HelperMethods.ShowMessage("No scheduled visit...");
                return;
            }


            // Gets all information about the visit
            var kins = context.NextOfKins.Where(v => v.VisitId == visit.VisitId).ToList();
            Cabin cabin = context.Cabins.Single(cab => cab.CabinId == camper.CabinId);
            Councelor councelor = context.Councelors.Single(counc => counc.Cabin.CabinId == cabin.CabinId);

            Console.Clear();
            Console.WriteLine($"You are visiting: {camper.GetFullName()} at {cabin.Name}");
            Console.WriteLine($"Visitors: ");
            foreach (NextOfKin k in kins)
            {
                Console.Write($"| {k.GetFullName()} |");
            }

            Console.WriteLine();
            Console.WriteLine($"Visitation hours: {visit.StartTime} - {visit.EndTime}");
            Console.WriteLine($"Councelor for information: {councelor.GetFullName()}. Phone Number: {councelor.PhoneNumber}");

            HelperMethods.ShowMessage();
        }

    }
}
