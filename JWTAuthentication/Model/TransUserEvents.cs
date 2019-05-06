using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Model
{
    public class TransUserEvents
    {
        public int id { get; set; }
        public string id_user { get; set; }
        public string id_news { get; set; }
        public string date_join { get; set; }
        public string status { get; set; }
    }
}
