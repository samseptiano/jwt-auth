using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PKMPosition
    {
        [Key]
        public int POSITION_ID { get; set; }
        public int KPI_ID { get; set; }
        public int KOMP_ID { get; set; }
        public Decimal BOBOT_TOTAL_KPI { get; set; }
        public Decimal BOBOT_TOTAL_KOMP { get; set; }
        public float ACTIVEYN { get; set; }
        public DateTime UPDDATE { get; set; }
        public String UPDUSER { get; set; }
    }
}
