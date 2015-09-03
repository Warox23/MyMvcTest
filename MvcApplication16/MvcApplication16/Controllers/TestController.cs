using MvcApplication16.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Timers;

namespace MvcApplication16.Controllers
{
   [Authorize]
    public class TestController : Controller
    {

        public readonly int QUESTIONSONPAGE = 3;
        static Dictionary<string, QPage> qs = new Dictionary<string, QPage>();
        QuestionContext db = new QuestionContext();
       
       
       
        //
        // GET: /Test/
        public ActionResult Index(int id = 1)
        {
           string currentUsName = HttpContext.User.Identity.Name;
            

           if (qs[currentUsName].questions[id-1]==null)
           {
               qs[currentUsName].questions[id - 1] = getQuestionsFromDB(id, qs[currentUsName].TestId );    
           }
             qs[currentUsName].CurrentPage = id;


           return View(qs[currentUsName]);
        }

        [HttpPost]
        public ActionResult Index(SimleQPage answer,EnumButtons Next)
        {
            string currentUsName = HttpContext.User.Identity.Name;

            if (!(qs[currentUsName].ComapareTestId(answer.TestId)))
                return View("Error");
            
         


            qs[currentUsName].questions[answer.CurrentPage - 1] = answer.questions;

            // if (Request.Form["Next"] == EnumButtons.Done.ToString())

            if (Next == EnumButtons.Done)
                return RedirectToRoute(new { controller = "test", action = "Ressult", TestId = qs[currentUsName].TestId });

            qs[currentUsName].CurrentPage++;


            return RedirectToRoute(new { controller = "test", action = "Index", id = qs[currentUsName].CurrentPage.ToString() });

        }

        public ActionResult Ressult(int TestId = 0)
        {
            string currentUsName = HttpContext.User.Identity.Name;
            List<Question> userAnswers = new List<Question>();
            List<Question> correctAnswers = new List<Question>();
           
        
        correctAnswers = db.Questions.Where(i => i.TestId == TestId).OrderBy(i => i.QuestionId).ToList();
     

        foreach (var i in qs[currentUsName].questions)
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
          qs.Remove(currentUsName);
          return View(RM);
        }


        public ActionResult InitTest(string TestName, int TestId)
       {
           if (!qs.Keys.Contains(HttpContext.User.Identity.Name))
           {
               ResoultSaveModel RSM = new ResoultSaveModel();
               RSM.TestName = TestName;
               RSM.UserName = HttpContext.User.Identity.Name;
               RSM.Mark = 0;
               RSM.Started = DateTime.Now;
               RSM.Finished = DateTime.Now;
               using (SaveContext a = new SaveContext())
               {
                   a.Save.Add(RSM);
                   a.SaveChanges();
               }

               var Qtmp = getQuestionsFromDB(1, TestId);

               int CountPages = (int)Math.Ceiling(((double)((db.Questions.SqlQuery("Select * from Questions where TestId=" +
                     TestId).ToList().Count) / (double)QUESTIONSONPAGE)));

               QPage tmp = new QPage(Qtmp, CountPages, QUESTIONSONPAGE, 1, TestId);
               qs.Add(RSM.UserName, tmp);
           }
           else if (!qs[HttpContext.User.Identity.Name].ComapareTestId(TestId))
               return View("Error");


           return RedirectToRoute(new {controller="Test", action = "index", id = 1 });
       }

       private List<Question> getQuestionsFromDB (int PageNum, int TestId)
        {
          List<Question>  questions = db.Questions.
               Where(i => i.TestId == TestId).OrderBy(i => i.QuestionId).
               Skip(QUESTIONSONPAGE * PageNum - QUESTIONSONPAGE).Take(QUESTIONSONPAGE).ToList();

          foreach (var ques in questions)
              ques.SetFalse();


            return questions;
        }
         

    }
}
