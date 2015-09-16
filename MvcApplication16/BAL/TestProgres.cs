using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model.DB;


namespace BAL
{
    public static class TestProgres
    {
        public static int QUESTIONSONPAGE = 3;
        public static Dictionary<string, QPage> PersonalTestProgress {get;set;}
        static DAL.QuestionContext db = new DAL.QuestionContext();



        /// <summary>
        /// Fill personal user page in dictionary from bd if it is empty
        /// </summary>
        /// <param name="userName">user wich asked for data</param>
        /// <param name="page">data will get for this page</param>
        public static void ReadQuestionsForUser(string userName,int page)
        {
            if (PersonalTestProgress[userName].questions[page - 1] == null)
            {
                PersonalTestProgress[userName].questions[page - 1] =
                    db.GetQuestionsFromDb(page, PersonalTestProgress[userName].TestId, QUESTIONSONPAGE);
            }

        }

        /// <summary>
        /// Set new current page
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="page">number of page</param>
        public static void UpdateUserPage(string userName, int page)
        {
            PersonalTestProgress[userName].CurrentPage = page;
        }

        /// <summary>
        /// check is valid test Id
        /// </summary>
        /// <param name="userInfo">SimleQPage contain all informationabout user fr validate id</param>
        /// <returns></returns>
        public static bool IsValidTest(SimleQPage userInfo)
        {
            if (PersonalTestProgress[userInfo.UssrName].ComapareTestId(userInfo.TestId))
                return true;

            return false;
        }
        /// <summary>
        /// Set page with answers in personal page in dictionary
        /// </summary>
        /// <param name="answers">User's answers</param>
        public static void UpdateQuestionsForUser(SimleQPage answers)
        {
            PersonalTestProgress[answers.UssrName].questions[answers.CurrentPage - 1] = answers.questions;
        }


        /// <summary>
        /// Returns activ test id for user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
         public static int GetTestId(string userName)
        {
            return PersonalTestProgress[userName].TestId;
        }
        /// <summary>
        /// increments current page
        /// returns new current page for user name
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns></returns>
        public static int IncremenCurrentPage (string userName)
        {
           return  ++PersonalTestProgress[userName].CurrentPage;
           
        }


        /// <summary>
        /// returns current page for user name
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns></returns>
        public static int GetCurrentPage(string userName)
        {
            return PersonalTestProgress[userName].TestId;
        }
        /// <summary>
        /// returns list of users unswers
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static List<Question> GetAnsweredList(string userName)
        {
            List<Question> userAnswers = new List<Question>();
            foreach (var i in PersonalTestProgress[userName].questions)
                foreach (var item in i)
                    userAnswers.Add(item);
            return userAnswers;
        }

        /// <summary>
        /// init class if it is not
        /// </summary>
        public static void InitTestProgres()
        {
            if (PersonalTestProgress == null)
            {
                PersonalTestProgress = new Dictionary<string, QPage>();
            }
        }


        /// <summary>
        /// Set into  database start time, user name, mark = 0, etc
        /// </summary>
        /// <param name="testName">test name</param>
        /// <param name="testId">test id</param>
        /// <param name="userName">user name</param>
        public static void SetStartPoint(string testName,int testId, string userName)
        {
            if (!IsStarted(userName))
            {
                ResoultSaveModel RSM = new ResoultSaveModel();
                RSM.SetDefaultValues(testName, userName);

                using (SaveContext a = new SaveContext())
                {
                    a.Save.Add(RSM);
                    a.SaveChanges();
                }

                var Qtmp = db.GetQuestionsFromDb(1, testId, QUESTIONSONPAGE);

                int CountPages = (int)Math.Ceiling(db.Questions.AsNoTracking().Where(x => x.TestId == testId).Count() / (double)QUESTIONSONPAGE);

                QPage tmp = new QPage(Qtmp, CountPages, QUESTIONSONPAGE, 1, testId,userName);
                PersonalTestProgress.Add(RSM.UserName, tmp);
            }

        }

        /// <summary>
        /// reurns true if user started test and not finished it yet
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsStarted(string userName)
        {
            if ( PersonalTestProgress.Keys.Contains(userName) )
                return true;

            return false;
        }
        /// <summary>
        /// removes user from dictionary by name
        /// </summary>
        /// <param name="userName">user name</param>
        public static void RemoveUser(string userName)
        {
            PersonalTestProgress.Remove(userName);
        }

    }
}
