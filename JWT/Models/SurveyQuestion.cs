using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class SurveyQuestion
    {
        [Key]
        public int SurveyQuestionID { get; set; }
        public int SurveyID { get; set; }
        public string Question { get; set; }
        public string QuestionType { get; set; }
        public string QuestionCategory { get; set; }
        public int QuestionSeq { get; set; }
        public string FGActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
