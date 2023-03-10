using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectsComparer;

namespace CampSleepaway.Model
{
    // Sets db table name to the name of this class (singular)
    [Table(nameof(Camper))]
    public class Camper : Participant
    {
        public int CamperId { get; set; }
        public int? CabinId { get; set; }
        public List<Visit>? Visit { get; set; }
        public Cabin Cabin { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string GetInfo()
        {
            string cabinName = "None";
            if (Cabin is not null)
            {
                cabinName = Cabin.Name;
            }

            string info = $"Name: {FirstName} {LastName}\n" +
                $"Phone Number: {PhoneNumber}\n" +
                $"Start Date: {StartDate:yyyy-MM-dd}\n" +
                $"End Date: {EndDate:yyyy-MM-dd}\n" +
                $"Date of Birth: {DateOfBirth:yyyy-MM-dd}\n" +
                $"Cabin: {cabinName}\n";

            return info;
        }
    }
}