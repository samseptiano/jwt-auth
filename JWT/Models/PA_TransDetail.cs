using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_TransDetail
    {
        //public string TRANSID { get; set; }
        public string KPINO { get; set; }        
        public string GRADESCORE { get; set;}        
        public string KPIcategory { get; set; }
        public string Evidence { get; set; }
        public List<PA_FileEvidence> PK_FileEvidences { get; set; }
    }
}
