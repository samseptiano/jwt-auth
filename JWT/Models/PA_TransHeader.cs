using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_TransHeader
    {
        [Key]
        public string TRANSID { get; set; }        
        public string USEREMPNIK { get; set; }            
        public List<PA_TransDetail> PK_TransDetails { get; set; }
    }
}
