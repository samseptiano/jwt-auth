using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_DevPlanHeader
    {
        public string DEVID { get; set; }
        public string PAID { get; set; }
        public string COMPID { get; set; }
        public string COMPNAME { get; set; }
        public List<PA_DevPlanDetail> devPlanDetail { get; set; }
    }
}
