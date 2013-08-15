using SCBWIFloridaJune.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCBWIFloridaJune.Controllers
{
    public class HomeController : Controller
    {
        private SCBWIContext site = new SCBWIContext();

        public ActionResult Index()
        {
            var c = from l in site.LisaWheelers
                        where l.WaitingList == false
                        where l.Cleared != null
                        select l;

            var count = c.Count();

            if (count >= 41)
            {
                ViewBag.left = "Waiting List!";
            }
            else
            {
                ViewBag.left = 41 - count;
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
