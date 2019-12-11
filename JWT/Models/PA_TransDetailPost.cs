using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_TransDetailPost
    {
        public string PAID { get; set; }        
        public string CP { get; set;}        
        public string SEMESTER { get; set; }
        public string KPIID { get; set; }
        public string COMPID { get; set; }
        public string EMPNIK { get; set; }
        public string EVIDENCES { get; set; }
        public string TARGET { get; set; }
        public string ACTUAL { get; set; }
        public string KPITYPE { get; set; }
    }

}
