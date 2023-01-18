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

            // Gets all distinct id's from both the camper table and the history table for camper
            var allCamperId = (from camper in campersHistoryDb
                                select camper.CamperId).AsEnumerable()
                        .Distinct().ToList();

            // Creates key value pairs with the id as the key and the camper names as values
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

            // Creates an anonymous object that includes the history of a camper and the PeriodStart and PeriodEnd fields in the db.
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

            foreach (var history in camperHistoryDb)
            {
                Console.WriteLine($"{history.PeriodEnd} - FirstName: {history.Camper.FirstName}| LastName: {history.Camper.LastName}| DateOfBirth: {history.Camper.DateOfBirth}| PhoneNumber: {history.Camper.PhoneNumber}| StartDate: {history.Camper.StartDate}| EndDate: {history.Camper.EndDate}");
                Console.WriteLine();
            }

            Console.WriteLine($"{camperHistoryDb[camperHistoryDb.Count - 1].PeriodStart} - The camper was created");
            HelperMethods.ShowMessage();
        }
    }
}
