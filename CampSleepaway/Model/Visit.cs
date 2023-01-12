using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Model
{
    [Table(nameof(Visit))]
    [PrimaryKey(nameof(VisitId))]
    public class Visit
    {
        public int VisitId { get; set; }
        public List<NextOfKin> Visitors { get; set; }
        public Camper Camper { get; set; }
        public int CamperId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
