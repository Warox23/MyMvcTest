using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication16.Models;

namespace MvcApplication16.Controllers
{
    public class TController : Controller
    {
        private QuestionContext db = new QuestionContext();

        //
        // GET: /T/

        public ActionResult Index()
        {

            var tests = db.Tests.ToList();

            foreach (var item in tests)
            {
                item.Questions = db.Questions.Where(x => x.TestId == item.Id).ToList();
                    //SqlQuery("Select * From Questions where TestId="+item.Id.ToString()).ToList();               
            }

            
            return View(tests);
        }

        //
        // GET: /T/Details/5

        public ActionResult Details(int id = 0)
        {
            TestModel testmodel = db.Tests.Find(id);
            if (testmodel == null)
            {
                return HttpNotFound();
            }
            return View(testmodel);
        }

        //
        // GET: /T/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /T/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TestModel testmodel)
        {
            if (ModelState.IsValid)
            {
                db.Tests.Add(testmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testmodel);
        }

        //
        // GET: /T/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TestModel testmodel = db.Tests.Find(id);
            if (testmodel == null)
            {
                return HttpNotFound();
            }
            return View(testmodel);
        }

        //
        // POST: /T/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TestModel testmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testmodel);
        }

        //
        // GET: /T/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TestModel testmodel = db.Tests.Find(id);
            if (testmodel == null)
            {
                return HttpNotFound();
            }
            return View(testmodel);
        }

        //
        // POST: /T/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestModel testmodel = db.Tests.Find(id);
            db.Tests.Remove(testmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}