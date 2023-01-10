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
                "Modify Phone Number",
                "Modify Start Date",
                "Modify End Date",
                "Move Camper to another Camp",
                "Delete Camper"
            });

            switch (selection)
            {
                case 0:
                    ModifyPhoneNumber(camper);
                    break;
                case 1:
                    ModifyStartDate(camper);
                    break;
                default:
                    break;
            }
        }

        private static void ModifyStartDate(Camper camper)
        {
            throw new NotImplementedException();
        }

        private static void ModifyPhoneNumber(Camper camper)
        {
            string newPhoneNumber = HelperMethods.GetString("Enter new phone number:");
            using CampSleepawayContext context = new();

            var camperDb = context.Campers.SingleOrDefault(c => c.CamperId == camper.CamperId);

            camperDb!.PhoneNumber = newPhoneNumber;

            context.SaveChanges();

            HelperMethods.ShowMessage("Updated phone number!");
        }
    }
}
