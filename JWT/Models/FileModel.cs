using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class FileModel
    {
        public int ID { get; set; }
        public byte[] FileData { get; set; }
        //public string[] FileData { get; set; }
        public string FileType { get; set; }

    }
}
