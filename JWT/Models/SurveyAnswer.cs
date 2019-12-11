using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class SurveyAnswer
    {
        [Key]
        public int SurveyAnswerID { get; set; }
        public int SurveyQuestionID { get; set; }
        public String Answer { get; set; }
        public String FGActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public String LastUpdateBy { get; set; }
    }
}
