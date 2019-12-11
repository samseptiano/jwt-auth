using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class Survey
    {

        [Key]
        public int SurveyID { get; set; }
        public String SurveyName { get; set; }
        public DateTime SurveyDateStart { get; set; }
        public DateTime SurveyDateEnd { get; set; }
        public double SurveyBobot { get; set; }
        public String FGActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public String LastUpdateBy { get; set; }
    }
}
