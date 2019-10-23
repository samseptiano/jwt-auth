using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class Event
    {
        [Key]
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
        public string FGActiveYN { get; set; }
        public string EventTopic { get; set; }
        public string trainerSignature { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
