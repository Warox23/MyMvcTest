using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAL
{
    public class QPage
    {
        private int countPages;

        public int ItemsOnPage { get; set; }

        public string UssrName { get; set; }

        private int tid;
        public int TestId { get {return tid;} }
        public int CurrentPage { get; set; }
        public List<Question>[] questions {get;set;}
        public int CountPages 
        {
            get
            {
                return countPages;
            }

            set
            {
                countPages = value;
            }
        }

        public QPage (List<Question> questions, int countPages,int items,int CurPageNum,int testId, string userName)
        {
            this.questions = new List<Question>[countPages];
            this.questions[CurPageNum-1] = questions;
            this.countPages = countPages;
            ItemsOnPage = items;
            CurrentPage = CurPageNum;
            tid = testId;
            UssrName = userName;
        }
        public QPage()
        {
            this.questions = null;
            countPages = 0;
            ItemsOnPage =0;
            CurrentPage = 0;
        }

        public bool ComapareTestId(int testId)
        {
            return testId == TestId;
        }

        

    }
}