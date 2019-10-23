using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class SurveyPeserta
    {
        [Key]
        public int SPID { get; set; }
        public int EventID { get; set; }
        public string EmpNIK { get; set; }
        public int SessionID { get; set; }
        public int SurveyID { get; set; }
        public string FGSurveyDoneYN { get; set; }
        public DateTime SurveyDoneDate { get; set; }
        public string FGActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }


        
    }
}
