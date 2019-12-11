using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PK_TransStatus
    {
        [Key]
        public string TRANSID { get; set; }
        public string APREMPNIK { get; set; }
        public string STATUS { get; set; }
    }
}
