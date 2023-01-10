using CampSleepaway.Controller;
using CampSleepaway.Data;
using CampSleepaway.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus
{
    public class CampersMenu
    {
        private static List<Camper> GetCampers()
        {
            using CampSleepawayContext context = new();

            List<Camper> campers = context.Campers.Include(cabin => cabin.Cabin).AsSplitQuery().ToList();

            return campers;
        }

        private static List<Cabin> GetCabins()
        {
            using CampSleepawayContext context = new();

            List<Cabin> cabins = context.Cabins.AsSplitQuery().ToList();

            return cabins;
        }

        public static void Menu()
        {
            Console.Clear();

            List<Camper> campers = GetCampers();
            List<string> options = new();

            foreach (var camper in campers)
            {
                options.Add(camper.GetFullName());
            }

            int selection = HelperMethods.ShowMenu("Which camper would you like to manage?", options);

            ManageCamperMenu(campers[selection]);
        }

        public static void ManageCamperMenu(Camper camper)
        {
            Console.Clear();
            string temp = "None";
            if (camper.Cabin is not null)
            {
                temp = camper.Cabin.Name;
            }

            string title = $"{camper.FirstName} {camper.LastName}\n" +
                $"Phone Number: {camper.PhoneNumber}\n" +
                $"Start Date: {camper.StartDate:yyyy-MM-dd}\n" +
                $"End Date: {camper.EndDate:yyyy-MM-dd}\n" +
                $"Date of Birth: {camper.DateOfBirth:yyyy-MM-dd}\n" +
                $"Camp: {temp}\n";

            int selection = HelperMethods.ShowMenu(title, new[]
            {
                "Update Phone Number",
                "Update Start Date",
                "Update End Date",
                "Move Camper to another Camp",
                "Delete Camper"
            });

            switch (selection)
            {
                case 0:
                    UpdatePhoneNumber(camper);
                    break;
                case 1:
                    var newDate = PersonController.ProccessStartDate();
                    CamperController.UpdateCamperStartDate(camper.CamperId, newDate);
                    break;
                case 2:
                    UpdateEndDate(camper);
                    break;
                case 3:
                    UpdateCamp(camper);
                    break;
                default:
                    break;
            }
        }

        private static void UpdateCamp(Camper camper)
        {
            Console.Clear();

            List<Cabin> cabins = GetCabins();
            List<string> cabinNames = cabins.Select(c => c.Name).ToList();

            int selection = HelperMethods.ShowMenu("Select new cabin", cabinNames);

            using CampSleepawayContext context = new();
            var camperDb = context.Campers.Single(c => c.CamperId == camper.CamperId);

            camperDb.CabinId = cabins[selection].CabinId;
            context.SaveChanges();

            HelperMethods.ShowMessage("Updated camp!");
        }

        private static void UpdateEndDate(Camper camper)
        {
            DateTime newDate = HelperMethods.GetDateTime("Enter a new end time:");

            using CampSleepawayContext context = new();
            var camperDb = context.Campers.SingleOrDefault(c => c.CamperId == camper.CamperId);

            camperDb!.EndDate = newDate;
            context.SaveChanges();

            HelperMethods.ShowMessage("Updated end date!");
        }

        private static void UpdatePhoneNumber(Camper camper)
        {
            string newPhoneNumber = HelperMethods.GetString("Enter new a phone number:");

            using CampSleepawayContext context = new();
            var camperDb = context.Campers.SingleOrDefault(c => c.CamperId == camper.CamperId);

            camperDb!.PhoneNumber = newPhoneNumber;

            context.SaveChanges();

            HelperMethods.ShowMessage("Updated phone number!");
        }
    }
}
