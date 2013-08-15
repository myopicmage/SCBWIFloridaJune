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
    public class PriceController : Controller
    {
        private SCBWIContext db = new SCBWIContext();

        //
        // GET: /Price/

        public ActionResult Index()
        {
            return View(db.Prices.ToList());
        }

        //
        // GET: /Price/Details/5

        public ActionResult Details(int id = 0)
        {
            Price price = db.Prices.Find(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        //
        // GET: /Price/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Price/Create

        [HttpPost]
        public ActionResult Create(Price price)
        {
            if (ModelState.IsValid)
            {
                db.Prices.Add(price);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(price);
        }

        //
        // GET: /Price/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Price price = db.Prices.Find(id);
            ViewBag.categories = DAL.GetCategoryBox();
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        //
        // POST: /Price/Edit/5

        [HttpPost]
        public ActionResult Edit(Price price)
        {
            if (ModelState.IsValid)
            {
                db.Entry(price).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(price);
        }

        //
        // GET: /Price/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Price price = db.Prices.Find(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        //
        // POST: /Price/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Price price = db.Prices.Find(id);
            db.Prices.Remove(price);
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