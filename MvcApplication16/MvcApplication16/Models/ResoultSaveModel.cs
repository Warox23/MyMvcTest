using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication16.Models
{
    public class ResoultSaveModel
    {
        public int Id { get; set; }

        [Display(Name = "Test name")]
        public string TestName { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
        public int Mark { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }


        public void SetDefaultValues(string testName,string userName)
        {
            TestName = testName;
            UserName = userName;
            Mark = 0;
            Started = DateTime.Now;
            Finished = DateTime.Now;
        }
    }
}