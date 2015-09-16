using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Model.DB
{
    public class RessultModel
    {
        private static SaveContext context = new SaveContext();
        private string userName;
        public double Mark { get; set; }
        public List<Question> AnsweredQuestions{get;set;}
        public List<Question> TrueQuestions {get;set;}

        public RessultModel() {}
        public RessultModel(List<Question> UserAnsweredQ,string userName,int testId)
        {
            AnsweredQuestions = UserAnsweredQ;
            this.userName = userName;
            TrueQuestions = new QuestionContext().Questions.Where(x => x.TestId == testId).ToList();
            CalculateMark();
        }

        /// <summary>
        /// saves result of testing
        /// </summary>
        public void SaveResult()
        {
            var RSM = context.Save.ToList().Last(i => i.UserName == userName);
            RSM.Mark = (int)Mark;
            RSM.Finished = DateTime.Now;

            context.Entry(RSM).State = EntityState.Modified;
            context.SaveChanges();
        }

        private void CalculateMark()
        {
            double mark=0;
            foreach (Question userQ in AnsweredQuestions)
              foreach (Question correctQ in TrueQuestions)
                  if (userQ.CompareTo(correctQ) == 0)
                  {
                      mark = 0;
              
                      for (int i = 0; i <correctQ.Answers.Count; i++)
                      {
                          if (correctQ.Answers[i].IsCorect == userQ.Answers[i].Checked)
                          {
                              if (correctQ.Answers[i].IsCorect)
                                  mark += 1d / correctQ.CorrectAnswers;
                          }
                          else
                              mark -= 1d / correctQ.CorrectAnswers;
                      }
                      if (mark < 0)
                          mark = 0;
                      Mark += mark;

                      break;
                  }
            Mark = Math.Round( Mark / TrueQuestions.Count * 100,2);
        }
    }
}