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
    public class CouncelorController
    {
        public static List<Councelor> GetCouncelors()
        {
            using CampSleepawayContext context = new();

            List<Councelor> councelors = context.Councelors.Include(cabin => cabin.Cabin).AsSplitQuery().ToList();

            return councelors;
        }

        public static List<Councelor> GetCouncelorHistory()
        {
            using CampSleepawayContext context = new();

            List<Councelor> councelor = context.Councelors.TemporalAll().AsSplitQuery().ToList();

            return councelor;
        }

        public static void AddCouncelor()
        {
            Console.Clear();

            Councelor councelor = new();

            councelor.FirstName = HelperMethods.GetString("First Name:");
            councelor.LastName = HelperMethods.GetString("Last Name:");
            councelor.PhoneNumber = HelperMethods.GetString("Phone Number:");
            councelor.StartDate = HelperMethods.GetDateTime("Start Date (yyyy-MM-dd):");
            councelor.EndDate = HelperMethods.GetDateTime("End Date (yyyy-MM-dd):");
            councelor.FavoriteNumber = HelperMethods.GetInt("Choose a favorite number:");

            string prompt = "Save changes to Db? (true/false)";
            bool save = HelperMethods.GetBool(prompt);
            if (save)
            {
                using CampSleepawayContext context = new();
                context.Councelors.Add(councelor);
                context.SaveChanges();

                HelperMethods.ShowMessage($"{councelor.FirstName} {councelor.LastName} was created!");
            }
            else
            {
                HelperMethods.ShowMessage("Person was not added...");
            }
        }
        public static void UpdatePhoneNumber(int councelorId, string newPhoneNumber)
        {
            using CampSleepawayContext context = new();
            var councelorDb = context.Councelors.Single(c => c.CouncelorId == councelorId);

            councelorDb.PhoneNumber = newPhoneNumber;

            context.SaveChanges();

            HelperMethods.ShowMessage("Updated phone number!");
        }

        public static void UpdateCouncelorStartDate(int councelorId, DateTime newStartTime)
        {
            using CampSleepawayContext context = new();
            var councelorDb = context.Councelors.Single(c => c.CouncelorId == councelorId);

            councelorDb.StartDate = newStartTime;
            context.SaveChanges();

            HelperMethods.ShowMessage("Updated start date!");
        }

        public static void UpdateCouncelorEndDate(int councelorId, DateTime newEndDate)
        {
            using CampSleepawayContext context = new();
            var councelorDb = context.Councelors.Single(c => c.CouncelorId == councelorId);

            councelorDb.EndDate = newEndDate;
            context.SaveChanges();

            HelperMethods.ShowMessage("Updated end date!");
        }

        public static void UpdateFavoriteNumber(int councelorId)
        {
            int newFavorite = HelperMethods.GetInt("Choose a new favorite number: ");

            using CampSleepawayContext context = new();
            var councelorDb = context.Councelors.Single(c => c.CouncelorId == councelorId);

            councelorDb.FavoriteNumber = newFavorite;
            context.SaveChanges();

            HelperMethods.ShowMessage($"Updated favorite number to: {newFavorite}");
        }

        public static void DeleteCouncelor(int councelorId)
        {
            bool delete = HelperMethods.GetBool("Are you sure? (true/false)");
            if (delete)
            {
                using CampSleepawayContext context = new();
                var councelorDb = context.Councelors.Single(c => c.CouncelorId == councelorId);

                context.Councelors.Remove(councelorDb);
                context.SaveChanges();

                HelperMethods.ShowMessage($"Councelor, {councelorDb.GetFullName()}, was deleted!");
            }
            else
            {
                HelperMethods.ShowMessage("Councelor was not deleted...");
            }
        }

        // Updates the Councelors Cabin Id and ensures that the previous value gets set to null, if there was one
        public static void UpdateCabin(int councelorId)
        {
            Console.Clear();

            List<Cabin> cabins = CabinController.GetCabins();
            List<string> cabinNames = cabins.Select(c => c.Name).ToList();

            int selection = HelperMethods.ShowMenu("Select new cabin", cabinNames);

            using CampSleepawayContext context = new();
            var oldCabinDb = context.Cabins.SingleOrDefault(c => c.CouncelorId == councelorId);

            if (oldCabinDb is not null)
            {
                oldCabinDb.CouncelorId = null;
            }

            var cabinDb = context.Cabins.Single(c => c.CabinId == cabins[selection].CabinId);

            cabinDb.CouncelorId = councelorId;
            context.SaveChanges();

            HelperMethods.ShowMessage("Updated councelor!");
        }
    }
}
