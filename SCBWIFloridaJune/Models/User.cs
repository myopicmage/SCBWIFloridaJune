using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using SCBWIFloridaJune.Models.ViewModels;

namespace SCBWIFloridaJune.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "You must enter your first name.")]
        [DisplayName("First Name")]
        [MaxLength(25, ErrorMessage = "Your first name cannot be longer than 25 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter your last name.")]
        [DisplayName("Last Name")]
        [MaxLength(40, ErrorMessage = "Your last name cannot be longer than 40 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must choose a badge nane.")]
        [DisplayName("Name You Want On Your Badge")]
        [MaxLength(65, ErrorMessage = "Your badge name cannot be longer than 65 characters.")]
        public string BadgeName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Required(ErrorMessage = "You must enter your address.")]
        [DisplayName("Address 1")]
        [MaxLength(100, ErrorMessage = "Your address cannot be longer than 100 characters.")]
        public string Address1 { get; set; }

        [DisplayName("Address 2")]
        [MaxLength(100, ErrorMessage = "Your address cannot be longer than 100 characters.")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "You must enter your city.")]
        [MaxLength(40, ErrorMessage = "Your city cannot be longer than 40 characters.")]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "You must choose your state.")]
        [MaxLength(2, ErrorMessage = "Abbreviations only please.")]
        [DisplayName("State/Province")]
        public string State { get; set; }

        [Required(ErrorMessage = "You must enter your zip or postal code.")]
        [MaxLength(7, ErrorMessage = "Your zip or postal code should not be longer than 7 characters.")]
        [DisplayName("Zip/Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Please select your country.")]
        [MaxLength(20, ErrorMessage = "Your country should not be longer than 6 characters.")]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "You must enter your email address.")]
        [MaxLength(50, ErrorMessage = "Your email address should not be longer than 50 characters.")]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You must enter your phone number.")]
        [DisplayName("Phone Number")]
        [MaxLength(16, ErrorMessage = "Digits only please.")]
        public string Phone { get; set; }

        [DisplayName("Special Needs")]
        [MaxLength(255)]
        public string SpecialNeeds { get; set; }

        [DisplayName("Total")]
        [DataType(DataType.Currency)]
        public double Total { get; set; }

        public string PayPalID { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Paid { get; set; }
        public DateTime? Cleared { get; set; }

        public string Account { get; set; }

        public int RegistrationID { get; set; }
        public Registration Registration { get; set; }

        public virtual ICollection<Critique> Critiques { get; set; }

        /*
         * Thanks to
         * http://www.techlicity.com/blog/dotnet-hash-algorithms.html
         * for hashing algorithm
         */
        public static string GetSHA1(string text)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);

            SHA1Managed hashString = new SHA1Managed();
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public EmailModel ToEmailModel() {
            var ToReturn = new EmailModel();

            ToReturn.Name = this.FullName;
            ToReturn.Address1 = this.Address1;
            ToReturn.Address2 = this.Address2;
            ToReturn.BadgeName = this.BadgeName;
            ToReturn.City = this.City;
            ToReturn.Country = this.Country;
            ToReturn.Email = this.Email;
            ToReturn.NumCritiques = this.Critiques == null ? "0" : this.Critiques.Count().ToString();
            ToReturn.Phone = this.Phone;
            ToReturn.PostalCode = this.PostalCode;
            ToReturn.SpecialNeeds = this.SpecialNeeds;
            ToReturn.State = this.State;
            ToReturn.Type = this.Registration.Type;
            ToReturn.Track = this.Registration.Track;
            ToReturn.Intensive = this.Registration.Intensive;
            ToReturn.FridayLunch = this.Registration.FridayLunch;
            ToReturn.SaturdayLunch = this.Registration.SaturdayLunch;

            return ToReturn;
        }
    }
}