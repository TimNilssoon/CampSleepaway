using CampSleepaway.Controller;
using CampSleepaway.Data;
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
            using CampSleepawayContext context = new();

            var camper = context.Campers.Single(c => c.CamperId == camperId);
            NextOfKinController.GetAllNextOfKin(camper);
        }
    }
}
