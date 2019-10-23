using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PK_FileEvidence
    {       
        public string TRANSID { get; set; }
        public string KPINO { get; set; }
        public string fileName { get; set; }
        public string fileStream { get; set; }       
        public string filePath { get; set; }
        public string fileType { get; set; }        
    }
}
