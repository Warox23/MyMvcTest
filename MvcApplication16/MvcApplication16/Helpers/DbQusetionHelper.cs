using MvcApplication16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication16.Helpers
{
   public static class DbQusetionHelper
    {
       public static List<Question> GetQuestionsFromDb(this QuestionContext db,int pageNum, int testId, int QUESTIONSONPAGE)
       {

           List<Question> questions = db.Questions.
              Where(i => i.TestId == testId).OrderBy(i => i.QuestionId).
              Skip(QUESTIONSONPAGE * pageNum - QUESTIONSONPAGE).Take(QUESTIONSONPAGE).ToList();

           foreach (var ques in questions)
               ques.SetFalse();

           return questions;

       }
    }
}