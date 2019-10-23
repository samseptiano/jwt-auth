using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PKApr
    {
        [Key]
        public int APR_ID { get; set; }
        public int TRANS_ID { get; set; }
        public int EMPNIK { get; set; }
        public int APR_EMPNIK { get; set; }
        public int APR_STATUS { get; set; }
        public int APR_LEVEL { get; set; }
        public DateTime UPDDATE { get; set; }
        public String UPDUSER { get; set; }

    }
}
