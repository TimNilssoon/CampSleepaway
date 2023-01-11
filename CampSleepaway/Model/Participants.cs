using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Model
{
    public abstract class Participant : Person
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
