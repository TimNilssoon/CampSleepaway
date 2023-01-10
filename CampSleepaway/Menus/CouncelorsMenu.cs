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
    public class CouncelorsMenu
    {
        private static List<Councelor> GetCouncelors()
        {
            using CampSleepawayContext context = new();

            List<Councelor> councelors = context.Councelors.Include(cabin => cabin.Cabin).AsSplitQuery().ToList();

            return councelors;
        }

        public static void Menu()
        {
            Console.Clear();

            List<Councelor> councelors = GetCouncelors();
            List<string> options = new();

            foreach (var councelor in councelors)
            {
                options.Add(councelor.GetFullName());
            }

            int selection = HelperMethods.ShowMenu("Which Councelor would you like to manage?", options);

            ManageCouncelorMenu(councelors[selection]);
        }

        public static void ManageCouncelorMenu(Councelor councelor)
        {
            Console.Clear();
            string temp = "None";
            if (councelor.Cabin is not null)
            {
                temp = councelor.Cabin.Name;
            }

            string title = $"{councelor.FirstName} {councelor.LastName}\n" +
                $"Phone Number: {councelor.PhoneNumber}\n" +
                $"Start Date: {councelor.StartDate:yyyy-MM-dd}\n" +
                $"End Date: {councelor.EndDate:yyyy-MM-dd}\n" +
                $"Favorite Number: {councelor.FavoriteNumber}" +
                $"Camp: {temp}\n";

            int selection = HelperMethods.ShowMenu(title, new[]
            {
                "Modify Phone Number",
                "Modify Start Date",
                "Modify End Date",
                "Move Councelor to another Camp",
                "Delete Councelor"
            });

            switch (selection)
            {
                case 0:
                    ModifyPhoneNumber(councelor);
                    break;
                default:
                    break;
            }
        }
    }
}
