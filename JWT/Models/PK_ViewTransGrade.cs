using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PK_ViewTransGrade
    {
        [Key]
        public string KPIGRADEID { get; set; } 
        public string hint { get; set; }
        public string KPIcategory { get; set; }
        public string KPINO { get; set; }
        public string TRANSID { get; set; }
    }
}
