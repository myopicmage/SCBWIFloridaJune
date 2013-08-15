using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SCBWIFloridaJune.Models;

namespace SCBWIFlorida.Controllers
{
    public class PayPalController : Controller
    {
        //
        // GET: /PayPal/

        private SCBWIContext scbwi = new SCBWIContext();

        public ActionResult IPN()
        {
            var formVals = new Dictionary<string, string>();
            formVals.Add("cmd", "_notify-validate");

            string response = GetPayPalResponse(formVals, false);
            DAL.Log("got IPN response", "IPN");

            if (response == "VERIFIED")
            {
                DAL.Log("IPN Response: " + Request["payment_status"], "IPN");
                DAL.Log("IPN Status: " + Request["custom"], "IPN");

                try
                {
                    var custom = Request["custom"];

                    var lisa = (from p in scbwi.LisaWheelers
                                where p.PayPalID == custom
                                select p).SingleOrDefault();

                    if (lisa == null)
                    {
                        DAL.Log("shit, fuck, lisa not found", "IPN");

                        return View();
                    }

                    if (Request["payment_status"] == "Completed")
                    {
                        DAL.Log("Completed, supposedly", "IPN");

                        lisa.Cleared = DateTime.UtcNow;

                        DAL.LWEmailComplete(lisa);
                    }
                    else if (Request["payment_status"] == "Pending")
                    {
                        DAL.Log("Pending, supposedly", "IPN");

                        lisa.Paid = DateTime.UtcNow;

                        DAL.LWEmailPending(lisa);
                    }
                    else if (Request["payment_status"] == "Denied")
                    {
                        DAL.Log("Declined, supposedly", "IPN");

                        DAL.LWEmailDeclined(lisa);
                    }

                    scbwi.Entry(lisa).State = EntityState.Modified;
                    scbwi.SaveChanges();

                    return View();
                }
                catch (Exception e)
                {
                    DAL.Log("Exception: " + e.Message, "IPN");
                }

                return View();
            }

            if (response == "INVALID")
            {
                DAL.Log("Invalid response! Bailing!", "IPN");
                DAL.Log(response, "IPN");
                
                string transactionID = Request["txn_id"];
                string sAmountPaid = Request["mc_gross_1"];
                string payer_email = Request["payer_email"];
                string stat = Request["payment_status"];
                string received = Request["payment_date"];

                return View();
            }

            return View();
        }

        string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {

            string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr"
                : "https://www.paypal.com/cgi-bin/webscr";


            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);

            StringBuilder sb = new StringBuilder();
            sb.Append(strRequest);

            foreach (string key in formVals.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, formVals[key]);
            }
            strRequest += sb.ToString();
            req.ContentLength = strRequest.Length;

            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://urlort#");
            //req.Proxy = proxy;
            //Send the request to PayPal and get the response
            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {
                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }

            return response;
        }
    }
}