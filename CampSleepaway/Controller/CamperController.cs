using CampSleepaway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Controller
{
    public class CamperController
    {
        public static void UpdateCamperStartDate(int id, DateTime newStartTime)
        {
            using CampSleepawayContext context = new();
            var camperDb = context.Campers.SingleOrDefault(c => c.CamperId == id);

            camperDb!.StartDate = newStartTime;
            context.SaveChanges();

            HelperMethods.ShowMessage("Updated start date!");
        }
    }
}
