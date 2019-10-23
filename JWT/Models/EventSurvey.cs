using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace JWT.Models
{
    public class EventSurvey
    {
 
        public int EventID { get; set; }
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; }
        public string ExternalEventCode { get; set; }
        public string EventDesc { get; set; }
        public string EventImage { get; set; }
        public string FGHasPasscodeYN { get; set; }
        public string Passcode { get; set; }
        public string FGHasSurveyYN { get; set; }
        public int SurveyID { get; set; }
        public string FGSurveyDoneYN { get; set; }
        public string FGAllSurveyDoneYN { get; set; }
        public string FGAbsenYN { get; set; }
    }
}