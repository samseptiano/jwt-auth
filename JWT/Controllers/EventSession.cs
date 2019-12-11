using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class EventSession
    {

        public int ESID { get; set; }
        public int EventID { get; set; }
        public int SessionID { get; set; }
        public string SessionName { get; set; }
        public DateTime SessionDate { get; set; }
        public String SessionPlace { get; set; }
        public String FGActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public String LastUpdateBy { get; set; }
    }
}
