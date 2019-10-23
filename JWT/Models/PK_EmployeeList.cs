using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PK_EmployeeList
    {
        [Key]
        private string id;
        private string empName;
        private string NIK;
        private string dept;
        private string NIKAtasan1;
        private string NIKAtasan2;
        private string tanggalKontrakAwal;
        private string tanggalKontrakAkhir;
        private string jobTitle;
        private string status;
    }
}
