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
    public class CabinController
    {
        public static List<Cabin> GetCabins()
        {
            using CampSleepawayContext context = new();

            List<Cabin> cabins = context.Cabins.AsSplitQuery().ToList();

            return cabins;
        }
    }
}
