using CampSleepaway.Data;
using CampSleepaway.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Controller
{
    public class CamperController
    {

        public static List<Camper> GetCampers()
        {
            using CampSleepawayContext context = new();

            List<Camper> campers = context.Campers.Include(cabin => cabin.Cabin).AsSplitQuery().ToList();

            return campers;
        }

        public static Camper GetCamperById(int id)
        {
            using CampSleepawayContext context = new();

            return context.Campers.Single(c => c.CamperId == id);
        }

        public static void AddCamper()
        {
            Console.Clear();

            Camper camper = new();

            camper.FirstName = HelperMethods.GetString("First Name:");
            camper.LastName = HelperMethods.GetString("Last Name:");
            camper.PhoneNumber = HelperMethods.GetString("Phone Number:");
            camper.DateOfBirth = HelperMethods.GetDateTime("Date of Birth (yyyy-MM-dd):");
            camper.StartDate = HelperMethods.GetDateTime("Start Date:");
            camper.EndDate = HelperMethods.GetDateTime("End Date:");

            string prompt = "Save changes to Db? (true/false)";
            bool save = HelperMethods.GetBool(prompt);
            if (save)
            {
                using CampSleepawayContext context = new();
                context.Campers.Add(camper);
                context.SaveChanges();

                HelperMethods.ShowMessage($"{camper.FirstName} {camper.LastName} was created!");
            }
            else
            {
                HelperMethods.ShowMessage("Person was not added...");
            }
        }

        

        public static void UpdateCamperStartDate(int id, DateTime newStartTime)
        {
            using CampSleepawayContext context = new();
            var camperDb = context.Campers.Single(c => c.CamperId == id);

            camperDb.StartDate = newStartTime;
            context.SaveChanges();

            HelperMethods.ShowMessage("Updated start date!");
        }

        public static void UpdateCamperEndDate(int camperId, DateTime newEndDate)
        {
            using CampSleepawayContext context = new();
            var camperDb = context.Campers.Single(c => c.CamperId == camperId);

            camperDb.EndDate = newEndDate;
            context.SaveChanges();

            HelperMethods.ShowMessage("Updated end date!");
        }

        public static void UpdatePhoneNumber(int camperId, string newPhoneNumber)
        {
            using CampSleepawayContext context = new();
            var camperDb = context.Campers.Single(c => c.CamperId == camperId);

            camperDb.PhoneNumber = newPhoneNumber;

            context.SaveChanges();

            HelperMethods.ShowMessage("Updated phone number!");
        }

        public static void DeleteCamper(int camperId)
        {
            bool delete = HelperMethods.GetBool("Are you sure? (true/false)");
            if (delete)
            {
                using CampSleepawayContext context = new();
                var camperDb = context.Campers.Single(c => c.CamperId == camperId);

                context.Campers.Remove(camperDb);
                context.SaveChanges();

                HelperMethods.ShowMessage($"Camper, {camperDb.GetFullName()}, was deleted!");
            }
            else
            {
                HelperMethods.ShowMessage("Camper was not deleted...");
            }
        }
    }
}
