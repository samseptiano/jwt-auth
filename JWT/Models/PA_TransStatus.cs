using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_TransStatus
    {
        [Key]
        public string PAID { get; set; }
        public string PASTATUS { get; set; }
    }
}
