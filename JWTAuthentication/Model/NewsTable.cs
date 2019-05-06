using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Model
{
    public class NewsTable
    {
        public int id { get; set; }
        public string news_name { get; set; }
        public string news_desc { get; set; }
        public string news_image { get; set; }
        public string news_author { get; set; }
        public string news_category { get; set; }
        public DateTime news_date { get; set; }
    
    }
}
