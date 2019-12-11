using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PKKPI
    {
        [Key]
        public int KPI_ID { get; set; }
        public String KPI_NAME { get; set; }
        public String KPI_DESK { get; set; }
        public int KPI_GRADE_ID { get; set; }
        public Decimal BOBOT { get; set; }
        public int NILAI { get; set; }
        public String EVIDENCE { get; set; }
        public DateTime UPDDATE { get; set; }
        public String UPDUSER { get; set; }
    }
}
