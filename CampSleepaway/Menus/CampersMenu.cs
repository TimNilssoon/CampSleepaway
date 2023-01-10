using CampSleepaway.Controller;
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

        public static void Menu()
        {
            Console.Clear();

            List<Camper> campers = CamperController.GetCampers();
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
                "Update Phone Number",
                "Update Start Date",
                "Update End Date",
                "Move Camper to another Camp",
                "Delete Camper",
                "Manage next of kin"
            });

            switch (selection)
            {
                case 0:
                    var newPhoneNumber = PersonController.ProccessPhoneNumber();
                    CamperController.UpdatePhoneNumber(camper.CamperId, newPhoneNumber);
                    break;
                case 1:
                    var newStartDate = PersonController.ProccessStartDate();
                    CamperController.UpdateCamperStartDate(camper.CamperId, newStartDate);
                    break;
                case 2:
                    var newEndDate = PersonController.ProccessEndDate();
                    CamperController.UpdateCamperEndDate(camper.CamperId, newEndDate);
                    break;
                case 3:
                    CamperController.UpdateCabin(camper.CamperId);
                    break;
                case 4:
                    CamperController.DeleteCamper(camper.CamperId);
                    break;
                case 5:
                    NextOfKinMenu.Menu(camper.CamperId);
                    break;
                default:
                    break;
            }
        }
    }
}
