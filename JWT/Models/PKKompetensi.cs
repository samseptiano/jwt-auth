using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PKKompetensi
    {
        [Key]
        public int KOMP_ID { get; set; }
        public String KOMP_NAME { get; set; }
        public String KOMP_DESK { get; set; }
        public Decimal BOBOT { get; set; }
        public int KOMP_RATE { get; set; }
        public int KOMP_GRADE_ID { get; set; }
        public Decimal NILAI { get; set; }
        public DateTime UPDDATE { get; set; }
        public String UPDUSER { get; set; }

    }
}
