using CampSleepaway.Data;
using CampSleepaway.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus.HistoryMenus
{
    public class ListCamperHistoryMenu
    {
        public static void Menu()
        {
            using CampSleepawayContext context = new();

            var camperHistoryDb = context.Campers.TemporalAll()
                .OrderBy(c => EF.Property<DateTime>(c, "PeriodEnd"))
                .Where(c => c.CamperId == 5);
        }
    }
}
