using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class Certificate
    {
        [Key]
        public string CertId { get; set; }
        public string eventId { get; set; }
        public string TraineeName { get; set; }
        public string TraineejobTitle { get; set; }
        public string TraineeNIK { get; set; }
        public string TrainerName { get; set; }
        public string TrainerJobTitle { get; set; }
        public string TrainingName { get; set; }
        public string TrainingDate { get; set; }
        public string DocumentCode { get; set; }
        public string Location { get; set; }
        public string CompanyName { get; set; }
        public string Barcode { get; set; }
        public string Topics { get; set; }
    }
}
