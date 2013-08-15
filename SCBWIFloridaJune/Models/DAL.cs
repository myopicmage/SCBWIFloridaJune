using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCBWIFloridaJune.Controllers;
using SCBWIFloridaJune.Models.ViewModels;

namespace SCBWIFloridaJune.Models
{
    public class DAL
    {
        public static LisaWheeler ConvertToLisaWheeler(User user)
        {
            var site = new SCBWIContext();
            var lisa = new LisaWheeler();

            lisa.FirstName = user.FirstName;
            lisa.LastName = user.LastName;
            lisa.Address1 = user.Address1;
            lisa.Address2 = user.Address2;
            lisa.City = user.City;
            lisa.State = user.State;
            lisa.Zip = user.PostalCode;
            lisa.Email = user.Email;
            lisa.Phone = user.Phone;
            lisa.Account = user.Account;

            return lisa;
        }

        public static SelectList MemberBox()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var member = new SelectListItem() 
            {
                Text = "Member ($100)",
                Value = "Member"
            };

            var nonmember = new SelectListItem()
            {
                Text = "Non Member ($125)",
                Value = "Non Member"
            };

            items.Add(member);
            items.Add(nonmember);

            return new SelectList(items, "Value", "Text");
        }

        public static SelectList CritiqueBox()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var member = new SelectListItem()
            {
                Text = "Yes ($45)",
                Value = "Yes"
            };

            var nonmember = new SelectListItem()
            {
                Text = "No ($0)",
                Value = "No"
            };

            items.Add(member);
            items.Add(nonmember);

            return new SelectList(items, "Value", "Text");
        }

        public static LisaWheeler GetLisaByAccount(string account)
        {
            var site = new SCBWIContext();

            var user = (from u in site.LisaWheelers
                        where u.Account == account
                        select u).SingleOrDefault();

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public static string CalcTotalByAccount(string account)
        {
            var site = new SCBWIContext();

            var user = (from u in site.Users
                        where u.Account == account
                        select u).SingleOrDefault();

            user.Registration = site.Registrations.Find(user.RegistrationID);

            double total = 0;

            var regtype = (from r in site.Prices
                           where r.Name == user.Registration.Type
                           select r).SingleOrDefault();

            total += regtype.Value;

            if (user.Registration.Intensive != null)
            {
                var intensive = (from i in site.Prices
                                 where i.Name == user.Registration.Intensive
                                 select i).SingleOrDefault();

                total += intensive.Value;
            }

            if (user.Critiques != null || user.Critiques.Count() > 0)
            {
                var critiques = (from p in site.Prices
                                 where p.Category == Category.Critique
                                 select p).FirstOrDefault();

                total += (critiques.Value * user.Critiques.Count());
            }

            user.Total = total;

            site.Entry(user).State = EntityState.Modified;
            site.SaveChanges();

            return total.ToString();
        }

        public static string GetCritiquePrice() {
            using (var site = new SCBWIContext()) {
                var critiques = (from p in site.Prices
                                where p.Category == Category.Critique
                                select p).FirstOrDefault();

                return critiques.Value.ToString();
            }
        }

        public static SelectList GetCritiqueTypeBox() {
            using (var site = new SCBWIContext()) {
                var critiques = from p in site.Prices
                                where p.Category == Category.Critique
                                select p;

                return new SelectList(critiques.ToList(), "Name", "Name");
            }
        }
        
        public static User GetUserByAccount(string account)
        {
            var site = new SCBWIContext();
            
            var user = (from u in site.Users
                        where u.Account == account
                        select u).SingleOrDefault();

            if (user == null)
            {
                return null;
            }

            user.Registration = site.Registrations.Find(user.RegistrationID);

            return user;
        }

        public static ICollection<Critique> GetUserCritiques(string account) {
            var site = new SCBWIContext();

            return GetUserByAccount(account).Critiques.ToList();
        }

        public static SelectList GetRegTypesBox() {
            using (var site = new SCBWIContext()) {
                var late = (from d in site.Dates
                            where d.Category == Category.Late
                            select d).SingleOrDefault();

                Category c = Category.Reg;

                System.Diagnostics.Debug.WriteLine(DateTime.UtcNow);

                if (late.Value < DateTime.UtcNow) {
                    c = Category.RegLate;
                }

                var reg = from p in site.Prices
                          where p.Category == c
                          select p;

                return new SelectList(reg.ToList(), "Name", "Name");
            }
        }

        public static SelectList GetIntensivesBox() {
            using (var site = new SCBWIContext()) {
                var intensives = from p in site.Prices
                                 where p.Category == Category.Intensive
                                 select p;

                return new SelectList(intensives.ToList(), "Name", "Name");
            }
        }

        public static SelectList GetTracksBox() {
            using (var site = new SCBWIContext()) {
                var tracks = site.Tracks.ToList();

                return new SelectList(tracks, "Name", "Display");
            }
        }

        public static SelectList GetLunchBox(Category cat) {
            using (var site = new SCBWIContext()) {
                var lunch = from i in site.Information
                            where i.Category == cat
                            select i;

                return new SelectList(lunch.ToList(), "Value", "Value");
            }
        }
        
        public static LastUser GetLastRegistration() {
            using (var site = new SCBWIContext()) {
                var us = (from u in site.Users
                         orderby u.Cleared descending
                         select u).FirstOrDefault();

                LastUser toReturn;

                if (us == null) {
                    toReturn = new LastUser {
                        Created = DateTime.UtcNow,
                        Name = "No registrations yet :("
                    };
                }
                else {
                    toReturn = new LastUser {
                        Created = us.Created,
                        Name = us.FullName
                    };
                }

                return toReturn;
            }
        }

        public static Date GetDate(Category c)
        {
            using (var site = new SCBWIContext())
            {
                var date = (from d in site.Dates
                            where d.Category == c
                            select d).SingleOrDefault();

                if (date == null)
                {
                    date = new Date
                    {
                        DateID = -1,
                        Name = "NO DATE SET",
                        Value = new DateTime(1, 1, 1)
                    };
                }

                return date;
            }
        }

        public static SelectList GetCategoryBox() {
            using (var site = new SCBWIContext()) {
                var categories = from Category c in Enum.GetValues(typeof (Category))
                                 select new {ID = c, Name = c.ToString()};

                return new SelectList(categories, "ID", "Name");
            }
        }

        /*
         * ------------------------------------------------------
         * SETTERS
         * ------------------------------------------------------
         * 
         */

        public static Status SetupStatus()
        {
            using (var site = new SCBWIContext())
            {
                var setup = (from s in site.Information
                             where s.Category == Category.Ready
                             select s).SingleOrDefault();

                if (setup == null)
                {
                    return Status.NotStarted;
                }
                if (setup.Value == "ready") {
                    return Status.Completed;
                }
                if (setup.Value == "in progress")
                {
                    return Status.InProgress;
                }

                return Status.NotStarted;
            }
        }

        public static void SetupConferenceDates(OpenCloseDate model) {
            using (var site = new SCBWIContext()) {
                var open = new Date {
                    Category = Category.Open,
                    Name = "Conference Open",
                    Value = model.OpenDate
                };

                var close = new Date {
                    Category = Category.Close,
                    Name = "Conference Close",
                    Value = model.CloseDate
                };

                var regopen = new Date {
                    Category = Category.RegistrationOpen,
                    Name = "Registration Open",
                    Value = model.RegOpenDate
                };

                var regclose = new Date {
                    Category = Category.RegistrationClose,
                    Name = "Registration Close",
                    Value = model.RegCloseDate
                };

                var late = new Date {
                    Category = Category.Late,
                    Name = "Late Registration Date",
                    Value = model.RegLateDate
                };

                var edit = new Date {
                    Category = Category.LastEdit,
                    Name = "Last Edit Date",
                    Value = model.LastEditDate
                };

                site.Dates.Add(open);
                site.Dates.Add(close);
                site.Dates.Add(regclose);
                site.Dates.Add(regopen);
                site.Dates.Add(late);
                site.Dates.Add(edit);

                site.Information.Add(new Information {
                    Category = Category.Ready,
                    Title = "Conference Setup Begun",
                    Value = "in progress"
                });

                site.SaveChanges();
            }
        }

        public static void SetupRegistrationTypes(RegTypes model) {
            using (var site = new SCBWIContext()) {
                site.Prices.Add(new Price {
                    Name = "Early Member Price",
                    Category = Category.Reg,
                    Value = model.EarlyMemberPrice
                });

                site.Prices.Add(new Price {
                    Name = "Late Member Price",
                    Category = Category.RegLate,
                    Value = model.LateMemberPrice
                });

                site.Prices.Add(new Price {
                    Name = "Early Non Member Price",
                    Value = model.EarlyNonMemberPrice,
                    Category = Category.Reg
                });

                site.Prices.Add(new Price {
                    Name = "Late Non Member Price",
                    Value = model.LateNonMemberPrice,
                    Category = Category.RegLate
                });

                site.SaveChanges();
            }
        }

        public static void SetupIntensives(IntensivePrices model) {
            using (var site = new SCBWIContext()) {
                site.Prices.Add(new Price {
                    Name = model.Intensive1,
                    Category = Category.Intensive,
                    Value = model.IntensivePrice
                });

                site.Prices.Add(new Price
                {
                    Name = model.Intensive2,
                    Category = Category.Intensive,
                    Value = model.IntensivePrice
                });

                site.Prices.Add(new Price
                {
                    Name = model.Intensive3,
                    Category = Category.Intensive,
                    Value = model.IntensivePrice
                });

                site.Prices.Add(new Price
                {
                    Name = model.Intensive4,
                    Category = Category.Intensive,
                    Value = model.IntensivePrice
                });

                site.Prices.Add(new Price {
                    Name = model.AuthorCritique,
                    Category = Category.Critique,
                    Value = model.CritiquePrice
                });

                site.Prices.Add(new Price {
                    Name = model.ArtCritique,
                    Category = Category.Critique,
                    Value = model.CritiquePrice
                });

                site.Prices.Add(new Price {
                    Name = model.DummyCritique,
                    Category = Category.Critique,
                    Value = model.CritiquePrice
                });

                site.SaveChanges();
            }
        }

        public static void SetupMeals(MealModel model) {
            using (var site = new SCBWIContext()) {
                site.Information.Add(new Information {
                    Category = Category.Lunch,
                    Title = model.Meal1,
                    Value = model.Meal1
                });

                site.Information.Add(new Information
                {
                    Category = Category.Lunch,
                    Title = model.Meal2,
                    Value = model.Meal2
                });

                site.Information.Add(new Information
                {
                    Category = Category.Lunch,
                    Title = model.Meal3,
                    Value = model.Meal3
                });

                site.SaveChanges();
            }
        }

        public static void SetupTracks(TrackModel model) {
            using (var site = new SCBWIContext()) {
                site.Tracks.Add(new Track {
                    Name = model.Track1,
                    Presenters = model.Track1Presenters
                });

                site.Tracks.Add(new Track
                {
                    Name = model.Track2,
                    Presenters = model.Track2Presenters
                });

                site.Tracks.Add(new Track
                {
                    Name = model.Track3,
                    Presenters = model.Track3Presenters
                });

                site.Tracks.Add(new Track
                {
                    Name = model.Track4,
                    Presenters = model.Track4Presenters
                });

                site.Tracks.Add(new Track
                {
                    Name = model.Track5,
                    Presenters = model.Track5Presenters
                });

                site.SaveChanges();
            }
        }
        
        public static void CompleteWizard() {
            using (var site = new SCBWIContext()) {
                var status = (from c in site.Information
                              where c.Category == Category.Ready
                              select c).SingleOrDefault();

                status.Value = "ready";

                site.Entry(status).State = EntityState.Modified;

                site.SaveChanges();
            }
        }

        public static void CreateUser(string accountname, User u) {
            using (var site = new SCBWIContext()) {
                var n = new User();

                n.Account = accountname;
                n.Address1 = u.Address1;
                n.Address2 = u.Address2;
                n.BadgeName = u.BadgeName;
                n.City = u.City;
                n.Country = u.Country;
                n.Email = u.Email;
                n.FirstName = u.FirstName;
                n.LastName = u.LastName;
                n.Phone = u.Phone;
                n.PostalCode = u.PostalCode;
                n.State = u.State;
                n.SpecialNeeds = u.SpecialNeeds;

                n.Created = DateTime.UtcNow;
                n.Paid = DateTime.UtcNow;
                n.Cleared = DateTime.UtcNow;

                n.PayPalID = User.GetSHA1(n.FullName + n.Created);

                n.Registration = new Registration();

                site.Users.Add(n);
                site.SaveChanges();
            }
        }

        public static void CreateRegistration(Registration reg, string account) {
            using (var site = new SCBWIContext()) {
                var user = (from u in site.Users
                            where u.Account == account
                            select u).First();

                user.Registration = reg;

                site.Entry(user).State = EntityState.Modified;
                site.SaveChanges();
            }
        }

        public static void CreateCritiques(string account, CritiqueView model) {
            var site = new SCBWIContext();

            var user = GetUserByAccount(account);

            var critiques = new Critique[model.NumCritiques];

            for (int i = 0; i < model.NumCritiques; i++) {
                critiques[i] = new Critique {
                    Type = model.CritiqueType,
                    UserID = user.UserID
                };

                site.Critiques.Add(critiques[i]);
            }

            site.SaveChanges();            
        }

        public static void Log(string origin, string message) {
            using (var site = new SCBWIContext()) {
                site.Logs.Add(new Log {
                    Message = message,
                    Origin = origin,
                    Time = DateTime.UtcNow
                });

                site.SaveChanges();
            }
        }

        public static void Email(User model) {
            new MailController().RegistrationFinished(model.ToEmailModel()).Deliver();
        }

        public static void PendingEmail(User model) {
            new MailController().PendingRegistration(model.ToEmailModel()).Deliver();
        }

        public static void LWEmailComplete(LisaWheeler lisa)
        {
            new MailController().LWComplete(lisa).Deliver();
        }

        public static void LWEmailPending(LisaWheeler lisa)
        {
            new MailController().LWPending(lisa).Deliver();
        }

        public static void LWEmailDeclined(LisaWheeler lisa)
        {
            new MailController().LWDeclined(lisa).Deliver();
        }
    }
}