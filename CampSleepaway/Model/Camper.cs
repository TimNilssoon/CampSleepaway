using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Model
{
    public class Camper : Person
    {
        public int CamperId { get; set; }
        public int CabinId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
