using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class SurveyAnswerPeserta
    {
       
        [Key]
        public int EventID { get; set; }
        public string EmpNIK { get; set; }
        public int SessionID { get; set; }
        public int SurveyID { get; set; }
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        public string AnswerEssay { get; set; }
        public String FGActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public String LastUpdateBy { get; set; }
        //SessionID int
    }
}
