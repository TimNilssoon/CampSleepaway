using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Model
{
    [Table(nameof(CamperNextOfKin))]
    public class CamperNextOfKin
    {
        public int CamperNextOfKinId { get; set; }
        public int CabinId { get; set; }
        public int CamperId { get; set; }
    }
}
