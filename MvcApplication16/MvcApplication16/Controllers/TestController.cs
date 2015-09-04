using MvcApplication16.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Timers;
using MvcApplication16.Helpers;

namespace MvcApplication16.Controllers
{
   [Authorize]
    public class TestController : Controller
    {

        public readonly int QUESTIONSONPAGE = 3;
        static Dictionary<string, QPage> PersonalTestProgress;
        QuestionContext db = new QuestionContext();
       
  
        //
        // GET: /Test/
        public ActionResult Index(int id = 1)
        {
            string currentUsName = GetUserName();
            

           if (PersonalTestProgress[currentUsName].questions[id-1]==null)
           {
               PersonalTestProgress[currentUsName].questions[id - 1] = db.GetQuestionsFromDb(id, PersonalTestProgress[currentUsName].TestId,QUESTIONSONPAGE );    
           }
            PersonalTestProgress[currentUsName].CurrentPage = id;

           return View(PersonalTestProgress[currentUsName]);
        }

       /// <summary>
       /// supports user testing
       /// </summary>
       /// <param name="questions"> questions, and other informations for current page </param>
        /// <param name="Next">  Next button needs to control testing or finish it </param>
       /// <returns></returns>

        [HttpPost]
        public ActionResult Index(SimleQPage answer, EnumButtons Next)
        {
            string currentUsName = GetUserName();

            if (!(PersonalTestProgress[currentUsName].ComapareTestId(answer.TestId)))
                return View("Error");

            PersonalTestProgress[currentUsName].questions[answer.CurrentPage - 1] = answer.questions;

            // if (Request.Form["Next"] == EnumButtons.Done.ToString())

            if (Next == EnumButtons.Done)
                return RedirectToRoute(new { controller = "test", action = "Ressult", TestId = PersonalTestProgress[currentUsName].TestId });

            PersonalTestProgress[currentUsName].CurrentPage++;
            return RedirectToRoute(new { controller = "test", action = "Index", id = PersonalTestProgress[currentUsName].CurrentPage.ToString() });
        }



       /// <summary>
       /// Calculate the result of testing, add in database, show result page
       /// </summary>
       /// <param name="TestId">test id </param>
       /// <returns></returns>

        public ActionResult Ressult(int TestId = 0)
        {
            string currentUsName = GetUserName();
            List<Question> userAnswers = new List<Question>();
            List<Question> correctAnswers = new List<Question>();
           
        
        correctAnswers = db.Questions.Where(i => i.TestId == TestId).OrderBy(i => i.QuestionId).ToList();
     
        foreach (var i in PersonalTestProgress[currentUsName].questions)
            foreach (var item in i)
                userAnswers.Add(item);

        var RM = new RessultModel(userAnswers, correctAnswers);

          using (SaveContext a = new SaveContext())
          {
              var RSM = a.Save.ToList().Last(i => i.UserName == currentUsName);
              RSM.Mark = (int)RM.Mark;
              RSM.Finished = DateTime.Now;

              if (ModelState.IsValid)
              {
                  a.Entry(RSM).State = EntityState.Modified;
                  a.SaveChanges();
              }
          }
          PersonalTestProgress.Remove(currentUsName);
          return View(RM);
        }

       /// <summary>
       /// colled before start testing,  need to control users
       /// </summary>
       /// <param name="TestName">name of current test</param>
       /// <param name="TestId"> id of current test</param>
       /// <returns></returns>
        public ActionResult InitTest(string TestName, int TestId)
       {
           if (PersonalTestProgress == null)
            {
                PersonalTestProgress = new Dictionary<string,QPage>();
            }

           if (!PersonalTestProgress.Keys.Contains(GetUserName()))
           {
               ResoultSaveModel RSM = new ResoultSaveModel();
               RSM.SetDefaultValues(TestName,GetUserName());

               using (SaveContext a = new SaveContext())
               {
                   a.Save.Add(RSM);
                   a.SaveChanges();
               }

               var Qtmp = db.GetQuestionsFromDb(1, TestId,QUESTIONSONPAGE);

               int CountPages = (int)Math.Ceiling(db.Questions.Where(x => x.TestId == TestId).Count() / (double)QUESTIONSONPAGE);

               QPage tmp = new QPage(Qtmp, CountPages, QUESTIONSONPAGE, 1, TestId);
               PersonalTestProgress.Add(RSM.UserName, tmp);
           }
           else if (!PersonalTestProgress[GetUserName()].ComapareTestId(TestId))
               return View("Error");



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
