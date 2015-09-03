using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication16.Models
{
    public class ResoultSaveModel
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string UserName { get; set; }
        public int Mark { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
    }
}