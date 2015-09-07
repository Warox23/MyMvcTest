using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication16.Models;
using WebMatrix.WebData;
using System.Web.Security;
using System.Web.Profile;

namespace MvcApplication16.Controllers
{

   
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Cabinet()
        {
           
            CabinetModel cabinet = new CabinetModel();

            using (var db = new QuestionContext() )
            {
                cabinet.tests = db.Tests.ToList();
            }

            using (var db = new SaveContext())
            {
                cabinet.RSM = db.Save.Where(x => x.UserName ==  HttpContext.User.Identity.Name).ToList();
            }

            return View(cabinet);
        }

        [Authorize(Roles="Admin")]
        public ActionResult AdminPanel()
        {
            ViewBag.Message = "Admin Panel.";

          

            var a = 5;

            var users = Membership.GetAllUsers();
  

            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AdminPanel(RoleModel newRoles)
        {
            ViewBag.Message = "Admin Panel.";


            var users = Membership.GetAllUsers();



            return View();
        }

    }
}
