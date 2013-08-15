using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SCBWIFloridaJune.Models;
using SCBWIFloridaJune.Models.ViewModels;
using WebMatrix.WebData;
using System.IO;

namespace SCBWIFloridaJune.Controllers
{
    public class ManageController : Controller
    {
        private SCBWIContext site = new SCBWIContext();
        //
        // GET: /Manage/

        [Authorize(Roles = "root")]
        public ActionResult Index()
        {
            if (DAL.SetupStatus() == Status.NotStarted) {
                return RedirectToAction("ConferenceSetupIntro");
            }
            
            return View(DAL.GetLastRegistration());
        }

        public ActionResult ConferenceSetupIntro() {
            return View();
        }

        [Authorize(Roles = "root")]
        public ActionResult ConferenceSetup() {
            ViewBag.categories = DAL.GetCategoryBox();
            return View();
        }

        [Authorize(Roles = "root")]
        [HttpPost]
        public ActionResult ConferenceSetup(OpenCloseDate model) {
            if (ModelState.IsValid) {
                DAL.SetupConferenceDates(model);

                return RedirectToAction("RegistrationTypes");
            }

            ModelState.AddModelError("", "Check your dates, one isn't parsing properly.");
            return View(model);
        }

        [Authorize(Roles = "root")]
        public ActionResult RegistrationTypes() {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "root")]
        public ActionResult RegistrationTypes(RegTypes model) {
            if (ModelState.IsValid) {
                DAL.SetupRegistrationTypes(model);
                return RedirectToAction("Intensives");
            }

            ModelState.AddModelError("", "Your values must all be digits only.");
            return View(model);
        }

        [Authorize(Roles = "root")]
        public ActionResult Intensives() {
            return View();
        }

        [Authorize(Roles = "root")]
        [HttpPost]
        public ActionResult Intensives(IntensivePrices model) {
            if (ModelState.IsValid) {
                DAL.SetupIntensives(model);
                return RedirectToAction("Meals");
            }

            ModelState.AddModelError("", "Your prices were not set correctly. Please try again.");
            return View(model);
        }

        [Authorize(Roles = "root")]
        public ActionResult Meals() {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "root")]
        public ActionResult Meals(MealModel model) {
            if (ModelState.IsValid) {
                DAL.SetupMeals(model);
                return RedirectToAction("Tracks");
            }

            ModelState.AddModelError("", "Your prices were not set correctly. Please try again.");
            return View(model);
        }

        [Authorize(Roles = "root")]
        public ActionResult Tracks() {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "root")]
        public ActionResult Tracks(TrackModel model) {
            if (ModelState.IsValid) {
                DAL.SetupTracks(model);
                return RedirectToAction("Finished");
            }

            ModelState.AddModelError("", "Sorry, there was an issue, please try again.");
            return View(model);
        }

        [Authorize(Roles = "root")]
        public ActionResult Finished() {
            DAL.CompleteWizard();

            return View();
        }

        [Authorize(Roles = "root")]
        public ActionResult SendEmail(int id)
        {
            var u = site.Users.Find(id);
            var r = site.Registrations.Find(u.RegistrationID);

            var email = new EmailModel(u, r);

            new MailController().ResentEmail(email).Deliver();

            return RedirectToAction("All");
        }

        [Authorize(Roles = "root")]
        public ActionResult All()
        {
            var registrations = (from s in site.Users
                                 where s.Registration.Type != ""
                                 select s).ToList();

            List<All> allreg = new List<All>();

            foreach (var reg in registrations)
            {
                var r = site.Registrations.Find(reg.RegistrationID);
                allreg.Add(new All(reg, r));
            }

            return View(allreg);
        }

        [Authorize(Roles = "root")]
        public ActionResult Dump()
        {
            var registrations = (from s in site.Users
                                 where s.Registration.Type != ""
                                 select s).ToList();

            List<All> toCSV = new List<All>();

            foreach (var reg in registrations)
            {
                var r = site.Registrations.Find(reg.RegistrationID);
                toCSV.Add(new All(reg, r));
            }

            var stream = new StreamWriter(Server.MapPath("~/Content/registration.csv"));
            var csv = new CsvHelper.CsvWriter(stream);
            csv.WriteRecords(toCSV);
            stream.Close();

            string projectpath = Server.MapPath("~/Content/registration.csv");

            return File(projectpath, "text/csv", "registrations.csv");
        }

        [Authorize(Roles = "root")]
        public ActionResult Delete(int id)
        {
            return View(site.Users.Find(id));
        }

        [Authorize(Roles = "root")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = site.Users.Find(id);
            site.Users.Remove(user);
            site.SaveChanges();
            return RedirectToAction("All");
        }

        [Authorize(Roles = "root")]
        public ActionResult QuickAdd()
        {
            ViewBag.regtypes = DAL.GetRegTypesBox();
            ViewBag.intensives = DAL.GetIntensivesBox();
            ViewBag.tracks = DAL.GetTracksBox();
            ViewBag.friday = DAL.GetLunchBox(Category.Lunch);
            ViewBag.saturday = DAL.GetLunchBox(Category.Lunch);
            
            return View();
        }

        [Authorize(Roles = "root")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuickAdd(All a)
        {
            if (ModelState.IsValid)
            {
                User temp = new User();

                temp.FirstName = a.FirstName;
                temp.LastName = a.LastName;
                temp.BadgeName = a.BadgeName;
                temp.Address1 = a.Address1;
                temp.Address2 = a.Address2;
                temp.City = a.City;
                temp.State = a.State;
                temp.PostalCode = a.PostalCode;
                temp.Created = DateTime.UtcNow;
                temp.Paid = DateTime.UtcNow;
                temp.Cleared = DateTime.UtcNow;
                temp.Total = a.Total;
                temp.Country = a.Country;

                temp.Registration = new Registration();

                temp.Registration.Type = a.Type;
                temp.Registration.Track = a.Track;
                temp.Registration.Intensive = a.Intensive;
                temp.Registration.FridayLunch = a.FridayLunch;
                temp.Registration.SaturdayLunch = a.SaturdayLunch;
                temp.Email = a.Email;
                temp.Phone = a.Phone;
                temp.SpecialNeeds = a.SpecialNeeds;

                site.Users.Add(temp);
                site.SaveChanges();

                return RedirectToAction("All");
            }

            return View(a);
        }
    }
}
