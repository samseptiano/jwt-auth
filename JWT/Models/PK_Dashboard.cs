using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PK_Dashboard
    {
        
        public string APREMPNIK { get; set; }
        public string APREMPNAME { get; set; }
        public string TOTAL { get; set; }
        public string SUDAH { get; set; }
        public string BELUM { get; set; }
    }
}
