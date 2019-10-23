using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PKTrans
    {
        [Key]
        public int TRANS_ID { get; set; }
        public String EMPNIK { get ; set;}
        public DateTime TGL_AWAL { get; set; }
        public DateTime TGL_AKHIR { get; set; }
        public int EMPTYPEID { get; set; }
        public int POSITIONID { get; set; }
        public String STATUS { get; set; }
        public Decimal NILAI { get; set; }
        public String RESULT { get; set; }
        public DateTime UPDDATE { get; set; }
        public String UPDUSER { get; set; }               
    }
}
