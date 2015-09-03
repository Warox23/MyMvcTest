using MvcApplication16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication16.Helpers
{
   public class DbHelper
    {
       public static List<Question> getQuestionsFromDB(int PageNum, int TestId, QuestionContext db, int QUESTIONSONPAGE)
       
        {
            List<Question> questions = db.Questions.
                 Where(i => i.TestId == TestId).OrderBy(i => i.QuestionId).
                 Skip(QUESTIONSONPAGE * PageNum - QUESTIONSONPAGE).Take(QUESTIONSONPAGE).ToList();

            foreach (var ques in questions)
                ques.SetFalse();


            return questions;
        }

    }
}