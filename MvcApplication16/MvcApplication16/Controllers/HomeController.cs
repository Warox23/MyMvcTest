using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication16.Models;

namespace MvcApplication16.Controllers
{

   
    public class HomeController : Controller
    {
        static int k = 0;
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            k++;
            ViewBag.count = k;

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
            List<TestModel> tests = new List<TestModel>();
            List<ResoultSaveModel> RSM = new List<ResoultSaveModel>();
            using (var db = new QuestionContext() )
            {
                tests = db.Tests.ToList();
            }

            using (var db = new SaveContext())
            {
                RSM = db.Save.Where(x => x.UserName ==  HttpContext.User.Identity.Name).ToList();
            }

            return View(tests);
        }

        [Authorize(Roles="Admin")]
        public ActionResult AdminPanel()
        {
            ViewBag.Message = "Admin Panel.";

            return View();
        }

    }
}
