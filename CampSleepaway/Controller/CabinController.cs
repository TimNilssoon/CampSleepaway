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
    public class CabinController
    {
        public static List<Cabin> GetCabins()
        {
            using CampSleepawayContext context = new();

            List<Cabin> cabins = context.Cabins.AsSplitQuery().ToList();

            return cabins;
        }

        public static void AddCamperToCabin(Camper camper)
        {
            Console.WriteLine();
            List<Cabin> cabins = GetCabins();
            List<string> cabinNames = cabins.Select(c => c.Name).ToList();

            int selection = HelperMethods.ShowMenu("Select new cabin", cabinNames);
            using CampSleepawayContext context = new();

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

            camper.CabinId = cabins[selection].CabinId;

            context.SaveChanges();
        }
    }
}
