using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication16.Models
{
    public class Question : IComparable
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int TestId { get; set; }

        private IList<AnswerVariant> answers;

        public virtual IList<AnswerVariant> Answers
        {
            get { 


                
                return answers; 
            
            
            } 
            set 
            {
                
                answers = value;
               
            } 
        }
    
        private int trueAnsver=0;
        [NotMapped]
        public int TrueAnsver 
        { 
            get
            {

                if (answers != null)
                {
                    trueAnsver = 0;
                    foreach (var i in answers)
                        if (i.IsCorect)
                            trueAnsver++;
                }
                return trueAnsver;
            }

            set
            {
                trueAnsver = value;
            }
        }
        public  Question()
        {
          
        }

        

        public void SetFalse()
        {
            foreach (var item in Answers)
            {
                item.SetFalse();
            }
        }

 
        public string GetStrList()
        {
            string a = "";
            foreach(AnswerVariant v in Answers)
            {
                a += v.Text + Environment.NewLine;
            }
            return a;
        }


        public override bool Equals (Object  obj)
        {
            if (this.QuestionId == ((Question)obj).QuestionId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return this.QuestionId;
        }

        public int CompareTo(object obj)
        {
            return this.QuestionId - ((Question)obj).QuestionId;           
        }
    }
}