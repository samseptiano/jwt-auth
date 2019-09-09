using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class PdfData
    {
        public string DocumentTitle { get; set; }

        public string TrainingDate { get; set; }

        public string TrainingName { get; set; }

        public string Trainer { get; set; }

        public string TrainerJobTitle { get; set; }

        public string Trainee { get; set; }

        public string TraineeNIK { get; set; }

        public string TraineejobTitle { get; set; }

        public string DocCode { get; set; }

        public string Location { get; set; }

        public string CompanyName { get; set; }

        public string Barcode { get; set; }

        public List<string> Topics { get; set; }
    }
}

