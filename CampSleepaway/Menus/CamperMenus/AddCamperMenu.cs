using CampSleepaway.Data;
using CampSleepaway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.CamperMenus
{
    public class AddCamperMenu
    {
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
    }
}
