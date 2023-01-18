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

            // Gets all distinct id's from both the cabin table and the history table for cabin
            var allCabinId = (from cabin in cabinHistoryDb
                                  select cabin.CabinId).AsEnumerable()
                        .Distinct().ToList();

            // Creates key value pairs with the id as the key and the cabin names as values
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

            // Creates an anonymous object that includes the history of a cabin and the PeriodStart and PeriodEnd fields in the db.
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
