using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PK_ViewTransDetail
    {
        //[Key]        
        public string KPIID { get; set; }
        public string TRANSID { get; set; }
        public string KPIDesc { get; set; }
        //public List<string> hint {get;set; }
        public List<PK_ViewTransGrade> PK_ViewTransGrades { get; set; }
        public string KPINO { get; set; }
        public string bobot { get; set; }
        public string nilai { get; set; }
        public string evidence { get; set; }
        public string CR { get; set; }
        public string KPIcategory { get; set; }
        public string GRADESCORE { get; set; }
        public List<PA_FileEvidence> PK_FileEvidences { get; set; }
    }
}
