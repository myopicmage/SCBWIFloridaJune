using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCBWIFloridaJune.Models;
using SCBWIFloridaJune.Models.ViewModels;

namespace SCBWIFloridaJune.Controllers
{
    public class RegisterController : Controller
    {   
        //
        // GET: /Register/

        public ActionResult Index()
        {
            var opendate = DAL.GetDate(Category.RegistrationOpen);
            var closedate = DAL.GetDate(Category.RegistrationClose);

            ViewBag.open = opendate.Value.ToString("D");

            if (opendate.DateID == -1 || closedate.DateID == -1)
            {
                return RedirectToAction("NotOpen");
            }

            if (opendate.Value > DateTime.UtcNow)
            {
                return RedirectToAction("NotOpen");
            }

            if (closedate.Value < DateTime.UtcNow)
            {
                return RedirectToAction("Closed");
            }

            if ((opendate.Value < DateTime.UtcNow) && (closedate.Value > DateTime.UtcNow))
            {
                return RedirectToAction("Start");
            }

            return RedirectToAction("NotOpen");
        }

        public ActionResult NotOpen()
        {
            return View();
        }

        public ActionResult Closed()
        {
            return View();
        }

        [Authorize]
        public ActionResult Start() {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Start(User u) {
            if (ModelState.IsValid) {
                DAL.CreateUser(User.Identity.Name, u);

                return RedirectToAction("Conference");
            }

            ModelState.AddModelError("", "Your information is slightly invalid. Please see below for individual errors.");
            return View(u);
        }

        [Authorize]
        public ActionResult Conference() {
            ViewBag.regtypes = DAL.GetRegTypesBox();
            ViewBag.intensives = DAL.GetIntensivesBox();
            ViewBag.tracks = DAL.GetTracksBox();
            ViewBag.friday = DAL.GetLunchBox(Category.Lunch);
            ViewBag.saturday = DAL.GetLunchBox(Category.Lunch);

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Conference(Registration reg) {
            if (ModelState.IsValid) {
                DAL.CreateRegistration(reg, User.Identity.Name);
                return RedirectToAction("Critique");
            }

            ModelState.AddModelError("", "Unfortunately, we could not process your registration. Try again.");
            return View(reg);
        }

        [Authorize]
        public ActionResult Critique() {
            ViewBag.critiques = DAL.GetCritiqueTypeBox();
            ViewBag.price = DAL.GetCritiquePrice();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Critique(CritiqueView model) {
            if (ModelState.IsValid) {
                DAL.CreateCritiques(User.Identity.Name, model);
                return RedirectToAction("Verify");
            }

            ModelState.AddModelError("", "We couldn't process your information, try again :(");
            return View(model);
        }

        [Authorize]
        public ActionResult Verify()
        {
            DAL.CalcTotalByAccount(User.Identity.Name);
            var user = DAL.GetUserByAccount(User.Identity.Name);
            if (user.Critiques != null) {
                ViewBag.displaycritiques = "do not";
            }

            return View(user);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Verify(User model) {
            return RedirectToAction("ToPayPal");
        }

        [Authorize]
        public ActionResult ToPayPal()
        {
            var user = DAL.GetUserByAccount(User.Identity.Name);
            ViewBag.paypalid = user.PayPalID;
            ViewBag.total = user.Total;
            return View();
        }

        public ActionResult Finished() {
            var site = new SCBWIContext();

            var user = DAL.GetUserByAccount(User.Identity.Name);

            var r = site.Registrations.Find(user.RegistrationID);

            var email = new EmailModel(user, r);

            new MailController().ResentEmail(email).Deliver();

            return View();
        }
    }
}
