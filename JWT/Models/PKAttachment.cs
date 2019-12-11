using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PKAttachment
    {
        [Key]
        public int KPI_ID { get; set; }
        public int ATT_ID { get; set; }
        public String FILE_NAME { get; set; }
        public DateTime UPDDATE { get; set; }
        public String UPDUSER { get; set; }
    }
}
