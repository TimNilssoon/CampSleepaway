using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Model
{
    [Table(nameof(CamperNextOfKin))]
    [PrimaryKey(nameof(CamperId), nameof(NextOfKinId))]
    public class CamperNextOfKin
    {
        public NextOfKin NextOfKin { get; set; }
        public int NextOfKinId { get; set; }
        public Camper Camper { get; set; }
        public int CamperId { get; set; }
    }
}
