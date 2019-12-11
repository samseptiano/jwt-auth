using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_TextEvidence
    {       
        public string PAId { get; set; }
        public string KPIId { get; set; }
        public string CompId { get; set; }
        public string semester { get; set; }       
        public string evidence { get; set; } 
        public string actual { get; set; }
        public string target { get; set; }
    }
}
