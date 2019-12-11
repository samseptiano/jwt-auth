using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_ViewTransDetail
    {
        //[Key]        
        public string empNIK { get; set; }
        public string PAId { get; set; }
        public string Id { get; set; }
        //public List<string> hint {get;set; }
        public List<PA_ViewTransGrade> PA_ViewTransGrades { get; set; }
        public string semester { get; set; }
        public string tahun { get; set; }
        public string bobot { get; set; }
        public string cp { get; set; }
        public string evidence { get; set; }
        public string target { get; set; }
        public string actual { get; set; }
        public string KPIName { get; set; }
        public string KPIPerspektif { get; set; }
        public string KPIType { get; set; }
        public string hasilFinal { get; set; }
        public string nilai { get; set; }
        public string status { get; set; }
        public string strength { get; set; }
        //public List<PA_FileEvidence> PK_FileEvidences { get; set; }
    }
}
