using DAL;
using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
   public static class DbQusetionHelper
    {
       public static List<Question> GetQuestionsFromDb(this DAL.QuestionContext db,int pageNum, int testId, int QUESTIONSONPAGE)
       {

           List<Question> questions = db.Questions.AsNoTracking().
              Where(i => i.TestId == testId).OrderBy(i => i.QuestionId).
              Skip(QUESTIONSONPAGE * pageNum - QUESTIONSONPAGE).Take(QUESTIONSONPAGE).ToList();

           return questions;

       }
    }
}