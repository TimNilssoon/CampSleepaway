using CampSleepaway.Controller;
using CampSleepaway.Data;
using Microsoft.EntityFrameworkCore;

namespace CampSleepaway.Menus.HistoryMenus
{
    public class ListCouncelorHistoryMenu
    {
        public static void Menu()
        {
            Console.Clear();

            var councelorHistoryDb = CouncelorController.GetCouncelorHistory();

            // Gets all distinct id's from both the councelor table and the history table for councelor
            var allCouncelorId = (from councelor in councelorHistoryDb
                              select councelor.CouncelorId).AsEnumerable()
                        .Distinct().ToList();

            // Creates key value pairs with the id as the key and the councelor names as values
            Dictionary<int, string> allCouncelorIdNames = new();

            foreach (var id in allCouncelorId)
            {
                var temp = councelorHistoryDb.Where(c => c.CouncelorId == id).First();
                allCouncelorIdNames.Add(temp.CouncelorId, temp.GetFullName());
            }

            int selection = HelperMethods.ShowMenu("Which councelor would you like to show the history of?", allCouncelorIdNames.Values);

            DisplayHistory(allCouncelorIdNames.Keys.ElementAt(selection));
        }

        private static void DisplayHistory(int councelorId)
        {
            Console.Clear();
            using CampSleepawayContext context = new();

            // Creates an anonymous object that includes the history of a councelor and the PeriodStart and PeriodEnd fields in the db.
            var councelorHistoryDb = context.Councelors.TemporalAll().Where(n => n.CouncelorId == councelorId)
                .OrderByDescending(c => EF.Property<DateTime>(c, "PeriodEnd"))
                .Select(c => new
                {
                    Councelor = c,
                    PeriodStart = EF.Property<DateTime>(c, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(c, "PeriodEnd")
                })
                .ToList();

            Console.WriteLine($"History of {councelorHistoryDb[0].Councelor.GetFullName()}\n");

            foreach (var history in councelorHistoryDb)
            {
                Console.WriteLine($"{history.PeriodEnd} - FirstName: {history.Councelor.FirstName} | LastName: {history.Councelor.LastName}" +
                    $"| PhoneNumber: {history.Councelor.PhoneNumber} | StartDate: {history.Councelor.StartDate}" +
                    $"| EndDate: {history.Councelor.EndDate} | FavoriteNumber: {history.Councelor.FavoriteNumber}");
                Console.WriteLine();
            }

            Console.WriteLine($"{councelorHistoryDb[councelorHistoryDb.Count - 1].PeriodStart} - The councelor was created");
            HelperMethods.ShowMessage();
        }
    }
}
