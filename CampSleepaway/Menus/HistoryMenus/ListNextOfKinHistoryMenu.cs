using CampSleepaway.Controller;
using CampSleepaway.Data;
using Microsoft.EntityFrameworkCore;

namespace CampSleepaway.Menus.HistoryMenus
{
    public class ListNextOfKinHistoryMenu
    {
        public static void Menu()
        {
            Console.Clear();

            var nextOfKinHistoryDb = NextOfKinController.GetNextOfKinHistory();

            // Gets all distinct id's from the temporal table
            var allNextOfKinId = (from nextOfKin in nextOfKinHistoryDb
                               select nextOfKin.NextOfKinId).AsEnumerable()
                        .Distinct().ToList();

            // Creates key value pairs from the distinct id's
            Dictionary<int, string> allNextOfKinIdNames = new();

            foreach (var id in allNextOfKinId)
            {
                var temp = nextOfKinHistoryDb.Where(c => c.NextOfKinId == id).First();
                allNextOfKinIdNames.Add(temp.NextOfKinId, temp.GetFullName());
            }

            int selection = HelperMethods.ShowMenu("Which next of kin would you like to show the history of?", allNextOfKinIdNames.Values);

            DisplayHistory(allNextOfKinIdNames.Keys.ElementAt(selection));
        }

        private static void DisplayHistory(int nextOfKinId)
        {
            Console.Clear();
            using CampSleepawayContext context = new();

            // Gets all the history of a camper, orders by descending on the PeriodEnd field
            var nextOfKinHistoryDb = context.NextOfKins.TemporalAll().Where(n => n.NextOfKinId == nextOfKinId)
                .OrderByDescending(c => EF.Property<DateTime>(c, "PeriodEnd"))
                .Select(c => new
                {
                    NextOfKin = c,
                    PeriodStart = EF.Property<DateTime>(c, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(c, "PeriodEnd")
                })
                .ToList();

            Console.WriteLine($"History of {nextOfKinHistoryDb[0].NextOfKin.GetFullName()}\n");

            foreach (var history in nextOfKinHistoryDb)
            {
                Console.WriteLine($"{history.PeriodEnd} - FirstName: {history.NextOfKin.FirstName}| LastName: {history.NextOfKin.LastName}| PhoneNumber: {history.NextOfKin.PhoneNumber}| RelationType: {history.NextOfKin.RelationType}");
                Console.WriteLine();
            }

            Console.WriteLine($"{nextOfKinHistoryDb[nextOfKinHistoryDb.Count - 1].PeriodStart} - The next of kin was created");
            HelperMethods.ShowMessage();
        }
    }
}