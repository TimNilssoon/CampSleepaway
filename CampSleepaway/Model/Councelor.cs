using System.ComponentModel.DataAnnotations.Schema;

namespace CampSleepaway.Model
{
    [Table(nameof(Councelor))]
    public class Councelor : Person
    {
        public int CouncelorId { get; set; }
        public Cabin Cabin { get; set; }
        public int FavoriteNumber { get; set; }

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
                $"Date of Birth: {FavoriteNumber}\n" +
                $"Cabin: {cabinName}\n";

            return info;
        }
    }
}
