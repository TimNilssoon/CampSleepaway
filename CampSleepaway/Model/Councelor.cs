using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Model
{
    [Table(nameof(Councelor))]
    public class Councelor : Person
    {
        public int CabinId { get; set; }
        public int MyProperty { get; set; }
    }
}
