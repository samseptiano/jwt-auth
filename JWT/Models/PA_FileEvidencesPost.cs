using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_FileEvidencesPost
    {       
        public string PAID { get; set; }
        public string KPIID { get; set; }
        public string COMPID { get; set; }
        public string SEMESTER { get; set; }       
        public string FILESTRING { get; set; }
        public string FILEEXT { get; set; }
        public string FILENAME { get; set; }
        public string EMPNIK { get; set; }

    }
}
