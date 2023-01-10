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

        public static void UpdateCabin(int camperId)
        {
            Console.Clear();

            List<Cabin> cabins = CabinController.GetCabins();
            List<string> cabinNames = cabins.Select(c => c.Name).ToList();

            int selection = HelperMethods.ShowMenu("Select new cabin", cabinNames);

            using CampSleepawayContext context = new();
            var camperDb = context.Campers.Single(c => c.CamperId == camperId);

            camperDb.CabinId = cabins[selection].CabinId;
            context.SaveChanges();

            HelperMethods.ShowMessage("Updated camp!");
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
