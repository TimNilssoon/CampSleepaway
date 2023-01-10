using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Controller
{
    public class PersonController
    {
        public static DateTime ProccessStartDate()
        {
            DateTime newDate = HelperMethods.GetDateTime("Enter a new start time:");

            return newDate;
        }
    }
}
