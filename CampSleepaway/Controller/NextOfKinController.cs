using CampSleepaway.Data;
using CampSleepaway.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Controller
{
    public static class NextOfKinController
    {
        public static void GetAllNextOfKin(Camper camper)
        {
            using CampSleepawayContext context = new();

            var temp = context.CamperNextOfKins.Include(c => c.Camper)
                                                .Include(n => n.NextOfKin).ToList();


        }
    }
}
