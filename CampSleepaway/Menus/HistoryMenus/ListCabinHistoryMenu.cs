using CampSleepaway.Controller;
using CampSleepaway.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.HistoryMenus
{
    public class ListCabinHistoryMenu
    {
        public static void Menu()
        {
            Console.Clear();

            var cabinHistoryDb = CabinController.GetCabinHistory();

            // Gets all distinct id's from the temporal table
            var allCabinId = (from cabin in cabinHistoryDb
                                  select cabin.CabinId).AsEnumerable()
                        .Distinct().ToList();

            // Creates key value pairs from the distinct id's
            Dictionary<int, string> allCabinIdNames = new();

            foreach (var id in allCabinId)
            {
                var temp = cabinHistoryDb.Where(c => c.CabinId == id).First();
                allCabinIdNames.Add(temp.CabinId, temp.Name);
            }

            int selection = HelperMethods.ShowMenu("Which cabin would you like to show the history of?", allCabinIdNames.Values);

            DisplayHistory(allCabinIdNames.Keys.ElementAt(selection));
        }

        private static void DisplayHistory(int cabinId)
        {
            Console.Clear();
            using CampSleepawayContext context = new();

            // Gets all the history of a cabin, orders by descending on the PeriodEnd field
            var cabinHistoryDb = context.Cabins.TemporalAll().Where(n => n.CabinId == cabinId)
                .OrderByDescending(c => EF.Property<DateTime>(c, "PeriodEnd"))
                .Select(c => new
                {
                    Cabin = c,
                    PeriodStart = EF.Property<DateTime>(c, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(c, "PeriodEnd")
                })
                .ToList();

            Console.WriteLine($"History of {cabinHistoryDb[0].Cabin.Name}\n");

            foreach (var history in cabinHistoryDb)
            {
                Console.WriteLine($"{history.PeriodEnd} - Name: {history.Cabin.Name}");
                Console.WriteLine();
            }

            Console.WriteLine($"{cabinHistoryDb[cabinHistoryDb.Count - 1].PeriodStart} - The cabin was created");
            HelperMethods.ShowMessage();
        }
    }
}
