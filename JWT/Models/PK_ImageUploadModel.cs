using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PK_ImageUploadModel
    {
        [Key]
        public string fileName;
        public string fileExtension;
        public string fileString;
    }
}
