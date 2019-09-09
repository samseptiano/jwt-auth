using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class Instructor
    {
        [Key]
        public int InstructorID { get; set; }
        public int EmpID { get; set; }
        public String EmpNIK { get; set; }
        public String InstructorName { get; set; }
        public String InstructorType { get; set; }
        public String FGActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public String LastUpdateBy { get; set; }
    }
}
