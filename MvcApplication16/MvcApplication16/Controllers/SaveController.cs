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
    public class SaveController : Controller
    {
        private SaveContext db = new SaveContext();

        //
        // GET: /Save/

        public ActionResult Index()
        {
            return View(db.Save.ToList());
        }

        //
        // GET: /Save/Details/5

        public ActionResult Details(int id = 0)
        {
            ResoultSaveModel resoultsavemodel = db.Save.Find(id);
            if (resoultsavemodel == null)
            {
                return HttpNotFound();
            }
            return View(resoultsavemodel);
        }

        //
        // GET: /Save/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Save/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResoultSaveModel resoultsavemodel)
        {
            if (ModelState.IsValid)
            {
                db.Save.Add(resoultsavemodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resoultsavemodel);
        }

        //
        // GET: /Save/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ResoultSaveModel resoultsavemodel = db.Save.Find(id);
            if (resoultsavemodel == null)
            {
                return HttpNotFound();
            }
            return View(resoultsavemodel);
        }

        //
        // POST: /Save/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ResoultSaveModel resoultsavemodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resoultsavemodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resoultsavemodel);
        }

        //
        // GET: /Save/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ResoultSaveModel resoultsavemodel = db.Save.Find(id);
            if (resoultsavemodel == null)
            {
                return HttpNotFound();
            }
            return View(resoultsavemodel);
        }

        //
        // POST: /Save/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResoultSaveModel resoultsavemodel = db.Save.Find(id);
            db.Save.Remove(resoultsavemodel);
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