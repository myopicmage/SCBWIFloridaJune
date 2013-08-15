using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCBWIFloridaJune.Models;
using System.IO;

namespace SCBWIFloridaJune.Controllers
{
    public class PictureBookController : Controller
    {
        private SCBWIContext db = new SCBWIContext();

        //
        // GET: /PictureBook/
        [Authorize(Roles = "root")]
        public ActionResult Manage()
        {
            var lisas = from l in db.LisaWheelers
                        where l.WaitingList == false
                        where l.Cleared != null
                        select l;

            return View(lisas);
        }

        [Authorize(Roles = "root")]
        public ActionResult All()
        {
            return View(db.LisaWheelers.ToList());
        }

        [Authorize(Roles = "root")]
        public ActionResult WaitingList()
        {
            var lisas = from l in db.LisaWheelers
                        where l.WaitingList == true
                        where l.Cleared != null
                        select l;

            return View(lisas);
        }

        [Authorize(Roles = "root")]
        public ActionResult Resend(int id)
        {
            var lisa = db.LisaWheelers.Find(id);

            DAL.LWEmailComplete(lisa);

            return RedirectToAction("Manage");
        }

        [Authorize(Roles = "root")]
        public ActionResult DumpToCSV()
        {
            var lisas = from l in db.LisaWheelers
                        where l.WaitingList == false
                        where l.Cleared != null
                        select l;

            var stream = new StreamWriter(Server.MapPath("~/Content/lw.csv"));
            var csv = new CsvHelper.CsvWriter(stream);
            csv.WriteRecords(lisas);
            stream.Close();

            string projectpath = Server.MapPath("~/Content/lw.csv");

            return File(projectpath, "text/csv", "lw.csv");
        }

        [Authorize(Roles = "root")]
        public ActionResult DumpWaitingList()
        {
            var lisas = from l in db.LisaWheelers
                        where l.WaitingList == true
                        where l.Cleared != null
                        select l;

            var stream = new StreamWriter(Server.MapPath("~/Content/waitinglist.csv"));
            var csv = new CsvHelper.CsvWriter(stream);
            csv.WriteRecords(lisas);
            stream.Close();

            string projectpath = Server.MapPath("~/Content/waitinglist.csv");

            return File(projectpath, "text/csv", "waitinglist.csv");
        }

        [Authorize(Roles = "root")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "root")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LisaWheeler lisa)
        {
            if (ModelState.IsValid)
            {
                lisa.Created = DateTime.UtcNow;
                lisa.Cleared = DateTime.UtcNow;
                lisa.WaitingList = false;

                db.LisaWheelers.Add(lisa);
                db.SaveChanges();

                return RedirectToAction("Manage");
            }

            return View(lisa);
        }

        //
        // GET: /PictureBook/Details/5
        [Authorize(Roles = "root")]
        public ActionResult Details(int id = 0)
        {
            LisaWheeler lisawheeler = db.LisaWheelers.Find(id);
            if (lisawheeler == null)
            {
                return HttpNotFound();
            }
            return View(lisawheeler);
        }

        //
        // GET: /PictureBook/Create
        [Authorize]
        public ActionResult Index()
        {
            var lisa = DAL.GetLisaByAccount(User.Identity.Name);

            if (lisa != null)
            {
                return RedirectToAction("IsThisYouLisa");
            }

            return RedirectToAction("CreateNoAccount");
        }

        [Authorize]
        public ActionResult IsThisYouLisa()
        {
            var lisa = DAL.GetLisaByAccount(User.Identity.Name);

            if (lisa != null)
            {
                return View(lisa);
            }

            return RedirectToAction("CreateNoAccount");
        }

        [Authorize]
        public ActionResult IsThisYou()
        {
            var user = DAL.GetUserByAccount(User.Identity.Name);

            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("CreateNoAccount");
        }

        [Authorize]
        public ActionResult CreateNoAccount()
        {
            ViewBag.memberbox = DAL.MemberBox();
            ViewBag.critiquebox = DAL.CritiqueBox();

            return View();
        }

        [Authorize]
        public ActionResult CreateWithAccount()
        {
            var user = DAL.GetUserByAccount(User.Identity.Name);
            LisaWheeler acct;

            if (user == null)
            {
                acct = DAL.GetLisaByAccount(User.Identity.Name);
            }
            else
            {
                acct = DAL.ConvertToLisaWheeler(user);
            }

            ViewBag.memberbox = DAL.MemberBox();
            ViewBag.critiquebox = DAL.CritiqueBox();

            return View(acct);
        }

        //
        // POST: /PictureBook/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateNoAccount(LisaWheeler lisawheeler)
        {
            if (ModelState.IsValid)
            {
                lisawheeler.Created = DateTime.UtcNow;
                lisawheeler.Account = User.Identity.Name;

                db.LisaWheelers.Add(lisawheeler);
                db.SaveChanges();
                return RedirectToAction("Verify");
            }

            return View(lisawheeler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateWithAccount(LisaWheeler lisawheeler)
        {
            if (ModelState.IsValid)
            {
                lisawheeler.Created = DateTime.UtcNow;
                lisawheeler.Account = User.Identity.Name;

                db.Entry(lisawheeler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Verify");
            }

            return View(lisawheeler);
        }

        [Authorize]
        public ActionResult Verify()
        {
            var lisa = DAL.GetLisaByAccount(User.Identity.Name);
            lisa.Total = lisa.Member == "Member" ? 100.00 : 125.00;
            if (lisa.Critique == "Yes")
            {
                lisa.Total += 45;
            }

            var count = db.LisaWheelers.Count();

            if (count >= 41)
            {
                lisa.WaitingList = true;
                ViewBag.wl = "true";
            }
            else
            {
                lisa.WaitingList = false;
            }

            db.Entry(lisa).State = EntityState.Modified;
            db.SaveChanges();

            return View(lisa);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Verify(LisaWheeler lisa)
        {
            var acct = db.LisaWheelers.Find(lisa.LisaWheelerID);
            acct.PayPalID = LisaWheeler.GetSHA1(acct.FirstName + acct.LastName + DateTime.UtcNow);
            db.Entry(acct).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ToPayPal");
        }

        [Authorize]
        public ActionResult ToPayPal()
        {
            var lisa = DAL.GetLisaByAccount(User.Identity.Name);
            ViewBag.paypalid = lisa.PayPalID;
            ViewBag.total = lisa.Total;

            return View();
        }

        //
        // GET: /PictureBook/Edit/5
        [Authorize(Roles = "root")]
        public ActionResult Edit(int id = 0)
        {
            LisaWheeler lisawheeler = db.LisaWheelers.Find(id);
            if (lisawheeler == null)
            {
                return HttpNotFound();
            }
            return View(lisawheeler);
        }

        //
        // POST: /PictureBook/Edit/5
        [Authorize(Roles = "root")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LisaWheeler lisawheeler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lisawheeler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lisawheeler);
        }

        //
        // GET: /PictureBook/Delete/5
        [Authorize(Roles = "root")]
        public ActionResult Delete(int id = 0)
        {
            LisaWheeler lisawheeler = db.LisaWheelers.Find(id);
            if (lisawheeler == null)
            {
                return HttpNotFound();
            }
            return View(lisawheeler);
        }

        //
        // POST: /PictureBook/Delete/5
        [Authorize(Roles = "root")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LisaWheeler lisawheeler = db.LisaWheelers.Find(id);
            db.LisaWheelers.Remove(lisawheeler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [Authorize(Roles = "root")]
        public ActionResult Clear()
        {
            var lisa = db.LisaWheelers.ToList();

            foreach (var l in lisa)
            {
                db.LisaWheelers.Remove(l);
            }

            db.SaveChanges();

            return RedirectToAction("Manage");
        }
    }
}