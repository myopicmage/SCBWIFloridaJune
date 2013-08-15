using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCBWIFloridaJune.Models;

namespace SCBWIFloridaJune.Controllers
{
    public class CritiqueController : Controller
    {
        private SCBWIContext db = new SCBWIContext();

        //
        // GET: /Critique/

        public ActionResult Index()
        {
            var critiques = db.Critiques.Include(c => c.User);
            return View(critiques.ToList());
        }

        //
        // GET: /Critique/Details/5

        public ActionResult Details(int id = 0)
        {
            Critique critique = db.Critiques.Find(id);
            if (critique == null)
            {
                return HttpNotFound();
            }
            return View(critique);
        }

        //
        // GET: /Critique/Create

        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        //
        // POST: /Critique/Create

        [HttpPost]
        public ActionResult Create(Critique critique)
        {
            if (ModelState.IsValid)
            {
                db.Critiques.Add(critique);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", critique.UserID);
            return View(critique);
        }

        //
        // GET: /Critique/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Critique critique = db.Critiques.Find(id);
            if (critique == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", critique.UserID);
            return View(critique);
        }

        //
        // POST: /Critique/Edit/5

        [HttpPost]
        public ActionResult Edit(Critique critique)
        {
            if (ModelState.IsValid)
            {
                db.Entry(critique).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", critique.UserID);
            return View(critique);
        }

        //
        // GET: /Critique/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Critique critique = db.Critiques.Find(id);
            if (critique == null)
            {
                return HttpNotFound();
            }
            return View(critique);
        }

        //
        // POST: /Critique/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Critique critique = db.Critiques.Find(id);
            db.Critiques.Remove(critique);
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