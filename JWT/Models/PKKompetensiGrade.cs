using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PKKompetensiGrade
    {
        [Key]
        public int KOMP_GRADE_ID { get; set; }
        public int KOMP_GRADE { get; set; }
        public String KOMP_GRADE_DESK { get; set; }
        public DateTime UPDDATE { get; set; }
        public String UPDUSER { get; set; }
    }
}
