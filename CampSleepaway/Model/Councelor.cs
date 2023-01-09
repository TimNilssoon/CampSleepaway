using System.ComponentModel.DataAnnotations.Schema;

namespace CampSleepaway.Model
{
    [Table(nameof(Councelor))]
    public class Councelor : Person
    {
        public int CouncelorId { get; set; }
        public Cabin Cabin { get; set; }
        public int FavoriteNumber { get; set; }
    }
}
