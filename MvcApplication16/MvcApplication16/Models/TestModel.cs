using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication16.Models
{
    public class TestModel
    {
        public int Id{get;set;}
        public string title { get; set; }
        public string Description { get; set; }

        public virtual IList<Question> Questions {get;set;}

        public TestModel ()
        {
            Questions = new List<Question>();
        }


    }
}