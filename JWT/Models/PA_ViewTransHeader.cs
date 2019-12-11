using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_ViewTransHeader
    {
        [Key]
        public string paId { get; set; }
        public string empNIK { get; set; }
        public string year { get; set; }
        public string status { get; set; }
        public string strength { get; set; }
        public string nilai { get; set; }
        public string hasilDN { get; set; }
        public string hasilFinal { get; set; }
        public string statusAtasan1 { get; set; }
        public string namaAtasan1 { get; set; }
        public string NIKAtasan1 { get; set; }
        public string emailAtasan1 { get; set; }
        public string statusAtasan2 { get; set; }
        public string namaAtasan2 { get; set; }
        public string NIKAtasan2 { get; set; }
        public string emailAtasan2 { get; set; }
        public List<PA_ViewTransDetail> PA_ViewTransDetail { get;set;}
        public List<PA_MDevPlan> PA_MDevPlan { get; set; }
        public List<PA_DevPlanHeader> PA_DevPlanH { get; set; }
    }
}
