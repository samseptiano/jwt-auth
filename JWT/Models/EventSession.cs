using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class EventSession
    {
        [Key]
        public int ESID { get; set; }
        public int EventID { get; set; }
        public int SessionID { get; set; }
        public string SessionName { get; set; }
        public DateTime SessionDateStart { get; set; }
        public DateTime SessionDateEnd { get; set; }
        public string SessionPlace { get; set; }
        public int InstructorID { get; set; }
        public int SurveyID { get; set; }
        public string FGActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
