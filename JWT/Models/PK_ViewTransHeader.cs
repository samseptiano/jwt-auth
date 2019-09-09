using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PK_ViewTransHeader
    {
        [Key]
        public string transID { get; set; }
        public string Nilai { get; set; }
        public string Star { get; set; }
        public string status { get; set; }
        public string statusName { get; set; }
        public string scoreName { get; set; }
        public string namaAtasan1 { get; set; }
        public string NIKatasan1 { get; set; }
        public byte[] fotoAtasan1 { get; set; }
        public string aprStatus1 { get; set; }        
        public string namaAtasan2 { get; set; }
        public string NIKatasan2 { get; set; }
        public byte[] fotoAtasan2 { get; set; }
        public string aprStatus2 { get; set; }
        public string empName { get; set; }
        public string NIK { get; set; }
        public string orgName { get; set; }
        public string jobTitleName { get; set; }
        public string periodeAwal { get; set; }
        public string periodeAkhir { get; set; }
        public string locationName { get; set; }         
        public List<PK_ViewTransDetail> PK_ViewTransDetail { get;set;}               
    }
}
