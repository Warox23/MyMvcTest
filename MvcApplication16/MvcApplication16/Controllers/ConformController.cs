using Common.Enums;
using Model.DB;
using MvcApplication16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication16.Controllers
{
    public class ConformController : Controller
    {
        //
        // GET,POST: /Conform/

        public ActionResult Index(ConformModel result)
        {

            if (result.But == EnumButtons.No)
               return RedirectToRoute(new {action = "Cabinet", controller ="Home"});

            if (result.But == EnumButtons.Yes)
                return RedirectToRoute(new {action = "InitTest", 
                    controller = "Test", TestId = result.TestId, TestName =result.TestName});

            return View(result);
        }
    }
}
