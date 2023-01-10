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
    public static class NextOfKinController
    {
        public static List<NextOfKin> GetAllNextOfKin(Camper camper)
        {
            using CampSleepawayContext context = new();

            var nextOfKins = context.CamperNextOfKins.Include(n => n.NextOfKin).Where(c => c.CamperId == camper.CamperId).ToList();

            List<NextOfKin> visitors = nextOfKins.Select(n => n.NextOfKin).ToList();

            return visitors;
        }

        public static void AddNextOfKin(int camperId)
        {
            Console.Clear();

            NextOfKin nextOfKin = new();

            nextOfKin.FirstName = HelperMethods.GetString("First Name:");
            nextOfKin.LastName = HelperMethods.GetString("Last Name:");
            nextOfKin.PhoneNumber = HelperMethods.GetString("Phone Number:");
            nextOfKin.StartDate = HelperMethods.GetDateTime("Start Date:");
            nextOfKin.EndDate = HelperMethods.GetDateTime("End Date:");
            nextOfKin.RelationType = HelperMethods.GetRelationType("Choose Relation:");

            string prompt = "Save changes to Db? (true/false)";
            bool save = HelperMethods.GetBool(prompt);
            if (save)
            {
                using CampSleepawayContext context = new();
                context.NextOfKins.Add(nextOfKin);
                context.CamperNextOfKins.Add(new CamperNextOfKin() { CamperId = camperId, NextOfKin = nextOfKin });
                context.SaveChanges();

                HelperMethods.ShowMessage($"{nextOfKin.FirstName} {nextOfKin.LastName} was created!");
            }
            else
            {
                HelperMethods.ShowMessage("Person was not added...");
            }
        }

        public static void UpdateNextOfKin(int nextOfKinId)
        {
            using CampSleepawayContext context = new();

            var nextOfKin = context.NextOfKins.Single(n => n.NextOfKinId == nextOfKinId);

            bool done = false;
            while (!done)
            {
                Console.Clear();
                int selection = HelperMethods.ShowMenu("What property would you like to change?",
                new[]{
                    "First Name",
                    "Last Name",
                    "Phone Number",
                    "Start Date",
                    "End Date",
                    "Relation Type",
                    "Save Changes"
                });

                Console.WriteLine("Input empty string to cancel selection");

                string stringInput = String.Empty;
                bool? boolInput;


                switch (selection)
                {
                    case 0:
                        stringInput = HelperMethods.GetString("First Name:");
                        if (!String.IsNullOrWhiteSpace(stringInput))
                        {
                            nextOfKin.FirstName = stringInput;

                        }
                        break;
                    case 1:
                        stringInput = HelperMethods.GetString("Last Name:");
                        if (!String.IsNullOrWhiteSpace(stringInput))
                        {
                            nextOfKin.LastName = stringInput;

                        }
                        break;
                    case 2:
                        stringInput = HelperMethods.GetString("Phone Number:");
                        if (!String.IsNullOrWhiteSpace(stringInput))
                        {
                            nextOfKin.PhoneNumber = stringInput;
                        }
                        break;
                    case 3:
                        DateTime newStartDate = HelperMethods.GetDateTime("Start Date:");
                        nextOfKin.StartDate = newStartDate;
                        break;
                    case 4:
                        DateTime newEndDate = HelperMethods.GetDateTime("End Date:");
                        nextOfKin.EndDate = newEndDate;
                        break;
                    case 5:
                        RelationType type = HelperMethods.GetRelationType("Choose Relation:");
                        break;
                    case 6:
                        context.SaveChanges();
                        done = true;
                        break;
                    default:
                        break;
                }
            }
        }

        public static void DeleteNextOfKin(int nextOfKinId)
        {
            bool sure = HelperMethods.GetBool("Are you sure? (true/false)");

            if (sure)
            {
                using CampSleepawayContext context = new();

                var nextOfKin = context.NextOfKins.Single(n => n.NextOfKinId == nextOfKinId);
                context.Remove(nextOfKin);

                context.SaveChanges();
            }
            else
            {
                HelperMethods.ShowMessage("Relative was not deleted...");
            }
        }
    }
}
