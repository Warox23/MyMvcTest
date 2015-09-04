using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication16.Models
{
    public class AnswerVariant
    {
        public AnswerVariant()
        {
            Checked = false;
        }
      
        [Key]
        public int AnswerId { get; set; }

        public int QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorect { get; set; }

        [NotMapped]
        public bool Checked { get; set; }







    }
}