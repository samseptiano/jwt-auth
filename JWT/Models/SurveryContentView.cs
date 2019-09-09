using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class SurveyContentView
    {

        //public SurveyQuestion surveyquestionVm { get; set; }
        //public SurveyAnswer surveyanswerVm { get; set; }

        //[Key]
        //public int SurveyQuestionID { get; set; }
        //public int SurveyID { get; set; }
        //public String Question { get; set; }
        //public int QuestionTypeID { get; set; }
        //public string QuestionCategory { get; set; }


        //public SurveyAnswer surveyanswerVm { get; set; }


        [Key]
        public int SurveyID { get; set; }
        public String SurveyName { get; set; }
        public DateTime SurveyDateStart { get; set; }
        public DateTime SurveyDateEnd { get; set; }
        public float SurveyBobot { get; set; }
        public string SurveyHeader { get; set; }

        //public SurveyQuestion surveyquestionVm { get; set; }
        //public SurveyAnswer surveyanswerVm { get; set; }

        public IEnumerable<SurveyQuestion> surveyquestionVm { get; set; }
        public IEnumerable<SurveyAnswer> surveyanswerVm { get; set; }

        //public IEnumerable<PlayerDto> TeamPlayers { get; set; }

    }
}
