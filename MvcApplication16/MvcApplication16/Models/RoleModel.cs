using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace MvcApplication16.Models
{


    public class User
    {
        public int UserId { get; set; }
        public List<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }

    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class RoleModel
    {

        public List<User> Users { get; set; }

        public RoleModel()
        {
            Users = new List<User>();
        }


        public void FillModelFromDb()
        {



        }

    }


    public class RoleContext : DbContext
    {

    }
}