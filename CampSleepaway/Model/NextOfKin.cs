using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Model
{
    public class NextOfKin : Person
    {
        public int NextOfKinId { get; set; }
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