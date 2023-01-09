using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Model
{
    [Table(nameof(Camper))]
    public class Camper : Person
    {
        public int CamperId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
