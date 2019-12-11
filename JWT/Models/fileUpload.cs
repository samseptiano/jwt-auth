using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class fileUpload
    {
            public string fileName { get; set; }
            public string extension { get; set; }
            public IFormFile Image { get; set; }

            public string PAID { get; set; }
            public string KPIID { get; set; }
            public string COMPID { get; set; }
            public string SEMESTER { get; set; }
            public string EMPNIK { get; set; }
    }
}
