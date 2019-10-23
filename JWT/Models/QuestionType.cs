using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Models
{
    public class QuestionType
    {
        [Key]
        public int QuestionTypeID { get; set; }
        public string QuestionTypeName { get; set; }

    }
}
