using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MvcApplication16.Models
{
    public class AdminCreateUserModel
    {
        public RegisterModel reg { get; set; }


        public List<string> RolesList { get; set; }


        public AdminCreateUserModel()
        {
          
        }
    }
}