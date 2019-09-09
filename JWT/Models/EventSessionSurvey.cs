using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class EventSessionSurvey
    {
        public int ESID { get; set; }
        public int EventID { get; set; }
        public int SessionID { get; set; }
        public string SessionName { get; set; }
        public DateTime SessionDateStart { get; set; }
        public DateTime SessionDateEnd { get; set; }
        public string SessionPlace { get; set; }
        public int InstructorID { get; set; }
        public string InstructorName { get; set; }
        public string FGHasSurveyYN { get; set; }
        public int SurveyID { get; set; }
        public string FGSurveyDoneYN { get; set; }
        public DateTime SurveyDoneDate { get; set; }
        public string EventType { get; set; }
        public byte[] FileData { get; set; }
        
    }
}
