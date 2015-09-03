using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication16.Models
{
    public class AnswerVariant
    {
      
        [Key]
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorect { get; set; }

       public void SetFalse()
        {
            IsCorect = false;
        }




    }
}