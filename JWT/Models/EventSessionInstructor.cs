using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class EventSessionInstructor
    {

        [Key]
        public int SIID { get; set; }
        public int EventID { get; set; }
        public int SessionID { get; set; }
        public int InstructorID { get; set; }
        public String FGActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public String LastUpdateBy { get; set; }
    }
}
