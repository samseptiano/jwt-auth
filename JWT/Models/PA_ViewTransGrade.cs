using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_ViewTransGrade
    {
        //[Key]
        public string kpiGradeID { get; set; }
        public string kpiID { get; set; }
        public string KPIGradeCode { get; set; }
        public string KPIGradeName { get; set; }
    }
}
