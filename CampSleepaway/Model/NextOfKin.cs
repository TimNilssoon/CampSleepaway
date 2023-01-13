using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Model
{
    [Table(nameof(NextOfKin))]
    public class NextOfKin : Person
    {
        public int NextOfKinId { get; set; }
        public int? VisitId { get; set; }
        public Visit? Visit { get; set; }
        public RelationType RelationType { get; set; }
    }
}

public enum RelationType
{
    Mother,
    Father,
    Sibling,
    Other
}