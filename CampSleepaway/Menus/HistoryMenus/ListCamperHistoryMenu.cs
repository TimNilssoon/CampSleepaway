using CampSleepaway.Controller;
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

            var campersHistoryDb = CamperController.GetCampersHistory();

            // Gets all distinct id's from the temporal table
            var allCamperId = (from camper in campersHistoryDb
                                select camper.CamperId).AsEnumerable()
                        .Distinct().ToList();

            // Creates key value pairs from the distinct id's
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
            var camperHistoryDb = context.Campers.TemporalAll().Where(c => c.CamperId == camperId)
                .OrderByDescending(c => EF.Property<DateTime>(c, "PeriodEnd"))
                .Select(c => new
                {
                    Camper = c,
                    PeriodStart = EF.Property<DateTime>(c, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(c, "PeriodEnd")
                })
                .ToList();

            Console.WriteLine($"History of {camperHistoryDb[0].Camper.GetFullName()}\n");

            // Compares the records and prints what has been updated
            var current = camperHistoryDb[0].Camper;

            for (int i = 0; i < camperHistoryDb.Count - 1; i++)
            {
                // Gets difference
                var diff = current.Compare(camperHistoryDb[i + 1].Camper);

                if (diff is not null)
                {
                    if (camperHistoryDb[i].PeriodEnd.Year != 9999)
                    {
                        Console.WriteLine($"{camperHistoryDb[i].PeriodEnd} - The {diff.MemberPath} field was changed from {diff.Value2} to {diff.Value1}");
                    }
                }

                current = camperHistoryDb[i + 1].Camper;
            }

            Console.WriteLine($"{camperHistoryDb[camperHistoryDb.Count - 1].PeriodStart} - The camper was created");
            HelperMethods.ShowMessage();
        }
    }
}
