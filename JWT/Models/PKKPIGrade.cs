using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PKKPIGrade
    {
        [Key]
        public int KPI_GRADE_ID { get; set; }
        public int KPI_GRADE { get; set; }
        public String KPI_GRADE_DESK { get; set; }
        public DateTime UPDDATE { get; set; }
        public String UPDUSER { get; set; }
    }
}
