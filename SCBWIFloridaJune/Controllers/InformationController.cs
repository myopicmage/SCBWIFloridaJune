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
    public class InformationController : Controller
    {
        private SCBWIContext db = new SCBWIContext();

        //
        // GET: /Information/

        public ActionResult Index()
        {
            return View(db.Information.ToList());
        }

        //
        // GET: /Information/Details/5

        public ActionResult Details(int id = 0)
        {
            Information information = db.Information.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        //
        // GET: /Information/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Information/Create

        [HttpPost]
        public ActionResult Create(Information information)
        {
            if (ModelState.IsValid)
            {
                db.Information.Add(information);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(information);
        }

        //
        // GET: /Information/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Information information = db.Information.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            }

            ViewBag.categories = DAL.GetCategoryBox();

            return View(information);
        }

        //
        // POST: /Information/Edit/5

        [HttpPost]
        public ActionResult Edit(Information information)
        {
            if (ModelState.IsValid)
            {
                db.Entry(information).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(information);
        }

        //
        // GET: /Information/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Information information = db.Information.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        //
        // POST: /Information/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Information information = db.Information.Find(id);
            db.Information.Remove(information);
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