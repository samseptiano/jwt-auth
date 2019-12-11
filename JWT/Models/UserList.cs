using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    //class untuk menampilkan karyawan di Employee List PK by NIK
    public class UserList
    {
        [Key]
        public string status { get; set; }
        public string empId { get; set; }
        public string empNiK { get; set; }
        public string empName { get; set; }
        public byte[] fotoKaryawan { get; set; }
        //public string Name { get; set; }
        public string positionCode { get; set; }
        public string positionName { get; set; }
        public string jobTitleCode { get; set; }
        public string jobTitleName { get; set; }
        public string locationCode { get; set; }
        public string locationName { get; set; }
        public string companyName { get; set; }
        public string companyCode { get; set; }
        public string orgCode { get; set; }
        public string orgName { get; set; }
        public string dateEnd { get; set; }
        public string dateStart { get; set; }
        public string joinDate { get; set; }
        public string positionId { get; set; }
        public string signDate { get; set; }
        public string bulanJatuhTempo { get; set; }
        public string empType { get; set; }
        public string posAtasan1 { get; set; }
        public string posAtasan2 { get; set; }
        public string NIKAtasan1 { get; set; }
        public string NIKAtasan2 { get; set; }
        public string jobTitleAtasan1 { get; set; }
        public string jobTitleAtasan2 { get; set; }
        public string namaAtasan1 { get; set; }
        public string namaAtasan2 { get; set; }
        public byte[] fotoAtasan1 { get; set; }
        public byte[] fotoAtasan2 { get; set; }
    }
}
