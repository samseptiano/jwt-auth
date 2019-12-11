using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_FileEvidence
    {       
        public string PAId { get; set; }
        public string KPIId { get; set; }
        public string CompId { get; set; }
        public string semester { get; set; }       
        public string fileString { get; set; } //buat filestring base64
        public string filename { get; set; }
        public string fileExt { get; set; }
    }
}
