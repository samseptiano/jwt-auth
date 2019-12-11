using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PA_TransDetailPostList
    {
    public string STRENGTH { get; set; }
    public string STATUS { get; set; }
    public string PAID { get; set; }
    public string NIKBAWAHAN { get; set; }
    public List<PA_TransDetailPost> lTransDetail { get; set; }
        public List<PA_DevPlanHeader> lDevPlanHeader { get; set; }
    }
}
