using CampSleepaway.Controller;
using CampSleepaway.Data;
using CampSleepaway.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.CamperMenus
{
    public class EditCamperMenu
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

            int selection = HelperMethods.ShowMenu("Which camper would you like to edit?", options);

            ManageCamperMenu(campers[selection]);
        }

        public static void ManageCamperMenu(Camper camper)
        {
            Console.Clear();

            string info = camper.GetInfo();

            int selection = HelperMethods.ShowMenu(info, new[]
            {
                "Update Phone Number",
                "Update Start Date",
                "Update End Date",
                "Move Camper to another Cabin",
                "Delete Camper",
                "Manage next of kin",
                "Schedule Visit"
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
                    CabinController.AddCamperToCabin(camper.CamperId);
                    break;
                case 4:
                    CamperController.DeleteCamper(camper.CamperId);
                    break;
                case 5:
                    NextOfKinMenu.Menu(camper.CamperId);
                    break;
                case 6:

                    break;
                default:
                    break;
            }
        }
    }
}
