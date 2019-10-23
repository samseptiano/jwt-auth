using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class EventPesertaAbsen
    {
        [Key]
        public int EAID { get; set; }
        public int EventID { get; set; }
        public string EmpNIK { get; set; }
        public DateTime EventDate { get; set; }
        public string StatusHadirYN { get; set; }
        public string FGActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }




    }
}
