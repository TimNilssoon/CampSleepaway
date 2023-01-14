using CampSleepaway.Data;
using CampSleepaway.Model;
using Microsoft.EntityFrameworkCore;
using ObjectsComparer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.HistoryMenus
{
    public class ListCamperHistoryMenu
    {
        public static void Menu()
        {
            Console.Clear();
            using CampSleepawayContext context = new();
            var campersHistoryDb = context.Campers.TemporalAll();

            var allCamperId = (from camper in campersHistoryDb
                                select camper.CamperId).AsEnumerable()
                        .Distinct().ToList();

            Dictionary<int,string> allCamperIdNames = new();

            foreach (var id in allCamperId)
            {
                var temp = campersHistoryDb.Where(c => c.CamperId == id).First();
                allCamperIdNames.Add(temp.CamperId, temp.GetFullName());
            }

            int selection = HelperMethods.ShowMenu("Which camper would you like to show the history of?", allCamperIdNames.Values);

            DisplayHistory(allCamperIdNames.Keys.ElementAt(selection));
        }

        private static void DisplayHistory(int camperId)
        {
            Console.Clear();
            using CampSleepawayContext context = new();

            // Gets all the history of a camper, orders by descending on the PeriodEnd field
            var camperHistoryDb = context.Campers.TemporalFromTo(new DateTime(1900, 1, 1).ToUniversalTime(), DateTime.UtcNow)
                .OrderByDescending(c => EF.Property<DateTime>(c, "PeriodEnd"))
                .Where(c => c.CamperId == camperId)
                .ToList();

            DateTime changedOn = EF.Property<DateTime>(camperHistoryDb[0], "PeriodEnd");
            Console.WriteLine($"History of {camperHistoryDb[0].GetFullName()}\n");

            // Compares the records and prints what has been updated
            Camper current = camperHistoryDb[0];

            for (int i = 0; i < camperHistoryDb.Count - 1; i++)
            {
                // Gets difference
                var diff = current.Compare(camperHistoryDb[i + 1]);

                if (diff is not null)
                {
                    Console.WriteLine($"The {diff.MemberPath} field was changed from {diff.Value2} to {diff.Value1}");
                }

                current = camperHistoryDb[i + 1];
            }

            HelperMethods.ShowMessage();
        }
    }
}
