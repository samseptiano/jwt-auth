using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PKMKompetensi
    {
        [Key]
        public int KOMP_ID { get; set; }
        public String KOMP_NAME { get; set; }
        public String KOMP_DESK { get; set; }
        public Decimal BOBOT { get; set; }
        public int KOMP_RATE { get; set; }
        public float ACTIVEYN { get; set; }
        public DateTime UPDDATE { get; set; }
        public String UPDUSER { get; set; }
    }
}
