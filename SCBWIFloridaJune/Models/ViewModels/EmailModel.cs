using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models.ViewModels
{
    public class EmailModel
    {
        [DisplayName("Name You Want On Your Badge")]
        public string BadgeName { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

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

        [DisplayName("Number of Critiques")]
        public string NumCritiques { get; set; }

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

        [DisplayName("Total")]
        public double Total { get; set; }

        public EmailModel() { }

        public EmailModel(User u, Registration r) {
            Type = r.Type;
            Intensive = r.Intensive;
            Track = r.Track;
            FridayLunch = r.FridayLunch;
            SaturdayLunch = r.SaturdayLunch;

            Name = u.FullName;
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

            NumCritiques = u.Critiques.Count().ToString();
        }
    }
}