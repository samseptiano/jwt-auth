using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class EventPeserta
    {
        [Key]
        public int EPID { get; set; }
        public int EventID { get; set; }
        public string EmpNIK { get; set; }
        public string StatusHadirYN { get; set; }
        public string FGSurveyDoneYN { get; set; }
        public DateTime SurveyDoneDate { get; set; }
        public string FGActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

    }
}
