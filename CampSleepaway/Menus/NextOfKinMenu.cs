using CampSleepaway.Controller;
using CampSleepaway.Data;
using CampSleepaway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus
{
    public static class NextOfKinMenu
    {
        public static void Menu(int camperId)
        {
            Console.Clear();

            using CampSleepawayContext context = new();

            var camper = context.Campers.Single(c => c.CamperId == camperId);
            List<NextOfKin> nextOfKin = NextOfKinController.GetAllNextOfKin(camper);

            Console.WriteLine("Relatives:");
            foreach (var kin in nextOfKin)
            {
                Console.WriteLine($"{kin.GetFullName()} - {kin.RelationType}");
            }

            Console.WriteLine();

            int chosenKinId;

            int selection = HelperMethods.ShowMenu("What would you like to do?", new[]
            {
                "Add new next of kin",
                "Update next of kin",
                "Delete next of kin",
            });

            switch (selection)
            {
                case 0:
                    NextOfKinController.AddNextOfKin(camperId);
                    break;
                case 1:
                    chosenKinId = SubMenu(camperId);
                    NextOfKinController.UpdateNextOfKin(chosenKinId);
                    break;
                case 2:
                    chosenKinId = SubMenu(camperId);
                    NextOfKinController.DeleteNextOfKin(chosenKinId);
                    break;
                default:
                    break;
            }
        }

        public static int SubMenu(int camperId)
        {
            Console.Clear();

            using CampSleepawayContext context = new();

            var camper = context.Campers.Single(c => c.CamperId == camperId);
            List<NextOfKin> nextOfKin = NextOfKinController.GetAllNextOfKin(camper);

            int selection = HelperMethods.ShowMenu("Choose a relative", nextOfKin.Select(n => n.GetFullName()));

            return nextOfKin[selection].NextOfKinId;
        }
    }
}
