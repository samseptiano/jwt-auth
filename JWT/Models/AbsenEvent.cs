using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class AbsenEvent
    {
 
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EmpNIK { get; set; }
        public DateTime EventDate { get; set; }
        public string EventType { get; set; }

    }
}
