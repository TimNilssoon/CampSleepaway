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

            Visit visit = new();

            DateTime startTime = HelperMethods.GetDateTime("Start Time (yyyy-MM-dd HH:mm):");
            DateTime endTime = HelperMethods.GetDateTime("End Time (yyyy-MM-dd HH:mm):");

            bool startTimeValid = ValidateStartTime(startTime);
            bool endTimeValid = ValidateEndTime(endTime, startTime);

            if (!startTimeValid)
            {
                HelperMethods.ShowMessage("Invalid start time...");
                return;
            }

            if (!endTimeValid)
            {
                HelperMethods.ShowMessage("Invalid end time...");
                return;
            }

            visit.StartTime = startTime;
            visit.EndTime = endTime;

            using CampSleepawayContext context = new();

            var visitDb = context.Visits.SingleOrDefault(v => v.CamperId == camperId);

            var nextOfKinDb = context.NextOfKins.Single(n => n.NextOfKinId == nextOfKin.NextOfKinId);

            if (visitDb is null)
            {
                var camperDb = context.Campers.Single(c => c.CamperId == camperId);

                visit.Camper = camperDb;
                visit.StartTime = startTime;
                visit.EndTime = endTime;
                context.Visits.Add(visit);

                visitDb = context.Visits.SingleOrDefault(v => v.CamperId == camperId);
            }
            else
            {
                visitDb.StartTime = startTime;
                visitDb.EndTime = endTime;
                nextOfKinDb.VisitId = visitDb.VisitId;
            }

            context.SaveChanges();

            HelperMethods.ShowMessage("Visit scheduled!");

        }

        private static bool ValidateStartTime(DateTime startTime)
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

        private static bool ValidateEndTime(DateTime endTime, DateTime startTime)
        {
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
