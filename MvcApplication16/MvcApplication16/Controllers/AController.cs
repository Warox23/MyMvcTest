using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication16.Models;
using Model.DB;
using DAL;

namespace MvcApplication16.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class AController : Controller
    {
        private DAL.QuestionContext db = new DAL.QuestionContext();

        //
        // GET: /A/

        public ActionResult Index()
        {
            return View(db.AnswerVariants.ToList());
        }

        //
        // GET: /A/Details/5

     
        public ActionResult Details(int id = 0)
        {
            AnswerVariant answervariant = db.AnswerVariants.Find(id);
            if (answervariant == null)
            {
                return HttpNotFound();
            }
            return View(answervariant);
        }

        //
        // GET: /A/Create

        public ActionResult Create()
        {
            ViewBag.id = RouteData.Values["id"];
            return View();
        }
 


        //
        // POST: /A/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnswerVariant answervariant)
        {
            if (ModelState.IsValid)
            {
                db.AnswerVariants.Add(answervariant);
                db.SaveChanges();
                return RedirectToRoute(new { controller = "A", action = "Create", id = answervariant.QuestionId });
            }

           return RedirectToRoute("Error");

        }

        //
        // GET: /A/Edit/5

        public ActionResult Edit(int id = 0)
        {
            
            AnswerVariant answervariant = db.AnswerVariants.Find(id);
            if (answervariant == null)
            {
                return HttpNotFound();
            }
            return View(answervariant);
        }

        //
        // POST: /A/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnswerVariant answervariant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answervariant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(answervariant);
        }

        //
        // GET: /A/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AnswerVariant answervariant = db.AnswerVariants.Find(id);
            if (answervariant == null)
            {
                return HttpNotFound();
            }
            return View(answervariant);
        }

        //
        // POST: /A/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnswerVariant answervariant = db.AnswerVariants.Find(id);
            db.AnswerVariants.Remove(answervariant);
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