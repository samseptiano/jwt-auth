using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace JWT.Models
{
    public class User
    {
        [Key]
        public string UserID { get; set; }
        public string Username { get; set; }
        //public string Name { get; set; }
        public string Password { get; set; }
        public string EmpNIK { get; set; }
        public string EmpEmail { get; set; }
        public string FGActiveYN { get; set; }
        public DateTime LastChangePassword { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
