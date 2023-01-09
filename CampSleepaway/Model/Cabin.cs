using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CampSleepaway.Model
{
    [Table(nameof(Cabin))]
    public class Cabin
    {
        public int CabinId { get; set; }
        public string Name { get; set; }
        public List<Camper>? Campers { get; set; }
        public Councelor? Councelor { get; set; }
        public int? CouncelorId { get; set; }
    }
}
