using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionMailer.Net.Mvc;
using SCBWIFloridaJune.Models;
using SCBWIFloridaJune.Models.ViewModels;

namespace SCBWIFloridaJune.Controllers
{
    public class MailController : MailerBase {
        private const string FromAddress = "SCBWI Florida June 2013 <registerbot@scbwiflorida.com>";
        private const string Mom = "i'd rather not";

        public EmailResult VerificationEmail(User model) {
            To.Add(model.Email);
            From = FromAddress;
            Subject = "Your SCBWI Florida June 2013 Registration";
            return Email("VerificationEmail", model);
        }

        public EmailResult RegistrationFinished(EmailModel model) {
            To.Add(model.Email);
            To.Add(Mom);
            From = FromAddress;
            Subject = "Your SCBWI Florida June 2013 Registration";
            return Email("RegistrationFinished", model);
        }

        public EmailResult PendingRegistration(EmailModel model) {
            To.Add(model.Email);
            To.Add(Mom);
            From = FromAddress;
            Subject = "Your SCBWI Florida June 2013 Registration";
            return Email("PendingRegistration", model);
        }

        public EmailResult ResentEmail(EmailModel model)
        {
            To.Add(model.Email);
            To.Add(Mom);
            From = FromAddress;
            Subject = "Your SCBWI Florida June 2013 Registration";
            return Email("ResentRegistration", model);
        }

        public EmailResult LWComplete(LisaWheeler lisa)
        {
            To.Add(lisa.Email);
            To.Add(Mom);
            From = FromAddress;
            Subject = "Your SCBWI Florida Lisa Wheeler Picture Book Boot Camp Registration";
            return Email("LWComplete", lisa);
        }

        public EmailResult LWPending(LisaWheeler lisa)
        {
            To.Add(lisa.Email);
            To.Add(Mom);
            From = FromAddress;
            Subject = "Your SCBWI Florida Lisa Wheeler Picture Book Boot Camp Registration";
            return Email("LWPending", lisa);
        }

        public EmailResult LWDeclined(LisaWheeler lisa)
        {
            To.Add(lisa.Email);
            To.Add(Mom);
            From = FromAddress;
            Subject = "Your SCBWI Florida Lisa Wheeler Picture Book Boot Camp Registration";
            return Email("LWDeclined", lisa);
        }
    }
}
