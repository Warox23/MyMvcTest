using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication16.Models
{
    public class RessultModel
    {
        public double Mark { get; set; }
        public List<Question> AnsweredQuestions{get;set;}
        public List<Question> TrueQuestions {get;set;}

        public RessultModel() {}
        public RessultModel(List<Question> UserAnsweredQ, List<Question> CorrectAnsweredQ )
        {
            AnsweredQuestions = UserAnsweredQ;
            TrueQuestions = CorrectAnsweredQ;
            CalculateMark();
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