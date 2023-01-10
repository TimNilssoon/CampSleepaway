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

        public static DateTime ProccessEndDate()
        {
            DateTime newDate = HelperMethods.GetDateTime("Enter a new end time:");

            return newDate;
        }

        public static string ProccessPhoneNumber()
        {
            string newPhoneNumber = HelperMethods.GetString("Enter new a phone number:");

            return newPhoneNumber;
        }
    }
}
