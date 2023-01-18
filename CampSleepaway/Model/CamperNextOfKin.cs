using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Model
{
    // Sets a composite key
    [PrimaryKey(nameof(CamperId), nameof(NextOfKinId))]
    public class CamperNextOfKin
    {
        public int CamperId { get; set; }
        public Camper Camper { get; set; }
        public int NextOfKinId { get; set; }
        public NextOfKin NextOfKin { get; set; }
    }
}
