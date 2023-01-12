using CampSleepaway.Controller;
using CampSleepaway.Data;
using CampSleepaway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.VistorMenus
{
    public class ScheduleVisitMenu
    {
        public static void Menu(int camperId)
        {
            Console.Clear();

            var camper = CamperController.GetCamperById(camperId);
            var kins = NextOfKinController.GetAllNextOfKin(camper);

            List<string> names = new();

            foreach (var kin in kins)
            {
                names.Add(kin.GetFullName());
            }

            int selection = HelperMethods.ShowMenu("Select a Next of Kin", names);

            ScheduleVisit(camperId, kins[selection]);
        }

        private static void ScheduleVisit(int camperId, NextOfKin nextOfKin)
        {
            Console.Clear();

            Visit visit = new()
            {
                StartTime = HelperMethods.GetDateTime("StartTime:"),
                EndTime = HelperMethods.GetDateTime("EndTime:")
            };



            using CampSleepawayContext context = new();

            var nextOfKinDb = context.NextOfKins.Single(n => n.NextOfKinId == nextOfKin.NextOfKinId);
            var camperDb = context.Campers.Single(c => c.CamperId == camperId);

            visit.Camper = camperDb;
            nextOfKinDb.Visit = visit;

            context.Visits.Add(visit);
            context.SaveChanges();

            HelperMethods.ShowMessage("Visit scheduled!");
        }

        private static bool StartTime(DateTime startTime)
        {
            bool output = false;

            DateTime latest = startTime.Date.AddHours(20);
            DateTime earlyest = startTime.Date.AddHours(10);

            if (startTime > latest || startTime < earlyest)
            {
                return false;
            }

            return true;
        }

        private static bool EndTime(DateTime endTime, DateTime startTime)
        {
            bool output = false;

            DateTime latest = endTime.Date.AddHours(20);
            DateTime earlyest = endTime.Date.AddHours(10);
            DateTime threeHours = startTime.AddHours(3);

            if (endTime > latest || endTime < earlyest || endTime < startTime || endTime > threeHours)
            {
                return false;
            }

            return true;
        }
    }
}
