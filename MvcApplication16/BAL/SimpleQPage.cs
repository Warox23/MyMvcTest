using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAL
{
    public class SimleQPage
    {
        private int countPages;

        public int ItemsOnPage { get; set; }

        public string UssrName { get; set; }

        private int tid;
        public int TestId { get { return tid; } set { tid = value; } }
        public int CurrentPage { get; set; }
        public List<Question> questions { get; set; }
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

        public SimleQPage(List<Question> questions, int countPages, int items, int CurPageNum, int testId)
        {
            this.questions= questions;
            this.countPages = countPages;
            ItemsOnPage = items;
            CurrentPage = CurPageNum;
            tid = testId;
        }
        public SimleQPage()
        {
            this.questions = new List<Question>();
            countPages = 0;
            ItemsOnPage = 0;
            CurrentPage = 0;
        }

        public bool ComapareTestId(int testId)
        {
            return testId == TestId;
        }



    }
}