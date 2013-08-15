using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models.ViewModels
{
    public class All
    {
        public int UserID { get; set; }
        
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Badge Name")]
        public string BadgeName { get; set; }

        [DisplayName("Address 1")]
        public string Address1 { get; set; }

        [DisplayName("Address 2")]
        public string Address2 { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("State/Province")]
        public string State { get; set; }

        [DisplayName("Zip/Postal Code")]
        public string PostalCode { get; set; }

        [DisplayName("Country")]
        public string Country { get; set; }

        [DisplayName("Email Address")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        [DisplayName("Special Needs")]
        public string SpecialNeeds { get; set; }

        [DisplayName("Total")]
        [DataType(DataType.Currency)]
        public double Total { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Paid { get; set; }
        public DateTime? Cleared { get; set; }

        [DisplayName("Registration Type")]
        public string Type { get; set; }

        [DisplayName("Intensive")]
        public string Intensive { get; set; }

        [DisplayName("Track")]
        public string Track { get; set; }

        [DisplayName("Friday Lunch")]
        public string FridayLunch { get; set; }

        [DisplayName("Saturday Lunch")]
        public string SaturdayLunch { get; set; }

        [DisplayName("Number of Critiques")]
        public string NumCritiques { get; set; }

        public All() { }

        public All(User u, Registration r) {
            UserID = u.UserID;

            Type = r.Type;
            Intensive = r.Intensive;
            Track = r.Track;
            FridayLunch = r.FridayLunch;
            SaturdayLunch = r.SaturdayLunch;

            FirstName = u.FirstName;
            LastName = u.LastName;
            BadgeName = u.BadgeName;
            Address1 = u.Address1;
            Address2 = u.Address2;
            City = u.City;
            State = u.State;
            PostalCode = u.PostalCode;
            Country = u.Country;
            Email = u.Email;
            Phone = u.Phone;
            SpecialNeeds = u.SpecialNeeds;
            Total = u.Total;
            Created = u.Created;
            Paid = u.Paid;
            Cleared = u.Cleared;
            NumCritiques = u.Critiques.Count().ToString();
        }
    }
}