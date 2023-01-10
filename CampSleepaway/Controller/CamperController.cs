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
        public static void UpdateCamperStartDate(int id, DateTime newStartTime)
        {
            using CampSleepawayContext context = new();
            var camperDb = context.Campers.SingleOrDefault(c => c.CamperId == id);

            camperDb!.StartDate = newStartTime;
            context.SaveChanges();

            HelperMethods.ShowMessage("Updated start date!");
        }

        public static void UpdateCamperEndDate(int camperId, DateTime newEndDate)
        {
            using CampSleepawayContext context = new();
            var camperDb = context.Campers.SingleOrDefault(c => c.CamperId == camperId);

            camperDb!.EndDate = newEndDate;
            context.SaveChanges();

            HelperMethods.ShowMessage("Updated end date!");
        }

        public static void UpdatePhoneNumber(int camperId, string newPhoneNumber)
        {
            using CampSleepawayContext context = new();
            var camperDb = context.Campers.SingleOrDefault(c => c.CamperId == camperId);

            camperDb!.PhoneNumber = newPhoneNumber;

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
    }
}
