using MvcApplication16.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Timers;
using Common.Enums;
using Model.DB;
using System.Data.Entity;
using DAL;
using BAL;

namespace MvcApplication16.Controllers
{
   [Authorize]
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Index(int id = 1)
        {
            string currentUsName = GetUserName();

            TestProgres.ReadQuestionsForUser(currentUsName,id);
            TestProgres.UpdateUserPage(currentUsName, id);

            return View(TestProgres.PersonalTestProgress[currentUsName]);
        }

       /// <summary>
       /// supports user testing
       /// </summary>
       /// <param name="questions"> questions and other informations for current page </param>
        /// <param name="Next">  Next button needs to control testing or finish it </param>
       /// <returns></returns>

        [HttpPost]
        public ActionResult Index(SimleQPage answer, EnumButtons Next)
        {
            if (!TestProgres.IsValidTest(answer))
                return View("Error");

            TestProgres.UpdateQuestionsForUser(answer);

            if (Next == EnumButtons.Done)
                return RedirectToRoute(new 
                { controller = "test", action = "Ressult", TestId = TestProgres.GetTestId(answer.UssrName) });

            return RedirectToRoute(new 
                { controller = "test", action = "Index", id = TestProgres.IncremenCurrentPage(answer.UssrName) });
        }



       /// <summary>
       /// Calculate the result of testing, add in database, show result page
       /// </summary>
       /// <param name="TestId">test id </param>
       /// <returns></returns>

        public ActionResult Ressult(int TestId = 0)
        {
            string currentUsName = GetUserName();

            List<Question> userAnswers = TestProgres.GetAnsweredList(currentUsName);

            var RM = new RessultModel(userAnswers, currentUsName,TestId);
            RM.SaveResult();

          TestProgres.RemoveUser(currentUsName);
          return View(RM);
        }


        /// <summary>
       /// colled before start testing,  need to control users
       /// </summary>
       /// <param name="TestName">name of current test</param>
       /// <param name="TestId"> id of current test</param>
       /// <returns></returns>
        public ActionResult InitTest(string testName, int testId)
       {
           TestProgres.InitTestProgres();
    
            TestProgres.SetStartPoint(testName,testId,GetUserName());


           return RedirectToRoute(new {controller="Test", action = "index", id = 1 });
       }
       /// <summary>
       /// returns current user name
       /// </summary>
       /// <returns>current user name</returns>
        private string GetUserName()
        {
            return HttpContext.User.Identity.Name;
        }
    }
}
