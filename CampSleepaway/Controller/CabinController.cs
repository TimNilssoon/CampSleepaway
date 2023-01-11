using Azure.Core;
using CampSleepaway.Data;
using CampSleepaway.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Controller
{
    public class CabinController
    {
        public static List<Cabin> GetCabins()
        {
            using CampSleepawayContext context = new();

            List<Cabin> cabins = context.Cabins.AsSplitQuery().ToList();

            return cabins;
        }

        public static void UpdateCabinName(int cabinId)
        {
            string newName = HelperMethods.GetString("New Cabin Name:");

            using CampSleepawayContext context = new();
            var cabin = context.Cabins.Single(c => c.CabinId == cabinId);

            cabin.Name = newName;

            context.SaveChanges();
        }

        public static void AddCabin()
        {
            using CampSleepawayContext context = new();

            string name = HelperMethods.GetString("Cabin Name:");

            context.Cabins.Add(new Cabin() { Name = name });

            context.SaveChanges();
        }

        public static void DeleteCabin(int cabinId)
        {
            bool delete = HelperMethods.GetBool("Are you sure? (true/false)");
            if (delete)
            {
                using CampSleepawayContext context = new();
                var cabinDb = context.Cabins.Single(c => c.CabinId == cabinId);

                context.Cabins.Remove(cabinDb);
                context.SaveChanges();

                HelperMethods.ShowMessage($"Camper, {cabinDb.Name}, was deleted!");
            }
            else
            {
                HelperMethods.ShowMessage("Camper was not deleted...");
            }
        }

        public static void AddCamperToCabin(int camperId)
        {
            Console.WriteLine();
            List<Cabin> cabins = GetCabins();
            List<string> cabinNames = cabins.Select(c => c.Name).ToList();

            int selection = HelperMethods.ShowMenu("Select new cabin", cabinNames);
            using CampSleepawayContext context = new();

            var camperDb = context.Campers.Single(c => c.CamperId == camperId);

            var campersDb = context.Campers.Where(c => c.CabinId == cabins[selection].CabinId).ToList();
            var cabinDb = context.Cabins.Single(c => c.CabinId == cabins[selection].CabinId);

            if (campersDb.Count == 4)
            {
                HelperMethods.ShowMessage("This cabin is already full, select another one...");
                return;
            }

            if (cabinDb.CouncelorId is null)
            {
                HelperMethods.ShowMessage("This cabin does not have a councelor, select another one...");
                return;
            }

            camperDb.CabinId = cabins[selection].CabinId;

            context.SaveChanges();
        }
    }
}
