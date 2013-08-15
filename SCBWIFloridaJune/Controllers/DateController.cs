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
    public class DateController : Controller
    {
        private SCBWIContext db = new SCBWIContext();

        //
        // GET: /Default1/

        [Authorize(Roles = "root")]
        public ActionResult Index()
        {
            return View(db.Dates.ToList());
        }

        //
        // GET: /Default1/Details/5
        [Authorize(Roles = "root")]
        public ActionResult Details(int id = 0)
        {
            Date date = db.Dates.Find(id);
            if (date == null)
            {
                return HttpNotFound();
            }
            return View(date);
        }

        //
        // GET: /Default1/Create
        [Authorize(Roles = "root")]
        public ActionResult Create() {
            ViewBag.categories = DAL.GetCategoryBox();
            
            return View();
        }

        //
        // POST: /Default1/Create
        [Authorize(Roles = "root")]
        [HttpPost]
        public ActionResult Create(Date date)
        {
            if (ModelState.IsValid)
            {
                db.Dates.Add(date);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(date);
        }

        //
        // GET: /Default1/Edit/5
        [Authorize(Roles = "root")]
        public ActionResult Edit(int id = 0)
        {
            Date date = db.Dates.Find(id);
            ViewBag.categories = DAL.GetCategoryBox();
            if (date == null)
            {
                return HttpNotFound();
            }
            return View(date);
        }

        //
        // POST: /Default1/Edit/5
        [Authorize(Roles = "root")]
        [HttpPost]
        public ActionResult Edit(Date date)
        {
            if (ModelState.IsValid)
            {
                db.Entry(date).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(date);
        }

        //
        // GET: /Default1/Delete/5
        [Authorize(Roles = "root")]
        public ActionResult Delete(int id = 0)
        {
            Date date = db.Dates.Find(id);
            if (date == null)
            {
                return HttpNotFound();
            }
            return View(date);
        }

        //
        // POST: /Default1/Delete/5
        [Authorize(Roles = "root")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Date date = db.Dates.Find(id);
            db.Dates.Remove(date);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "root")]
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}