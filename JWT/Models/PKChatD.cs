using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PKChatD
    {
        [Key]
        public int CHAT_D_ID { get; set; }
        public int CHAT_ID { get; set; }
        public String APPR1 { get; set; }
        public String APPR1_CHAT { get; set; }
        public DateTime APPR1_DATE { get; set; }
        public String APPR2 { get; set; }
        public String APPR2_CHAT { get; set; }
        public DateTime APPR2_DATE { get; set; }
    }
}
