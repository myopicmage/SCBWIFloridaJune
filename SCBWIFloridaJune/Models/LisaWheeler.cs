using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SCBWIFloridaJune.Models
{
    public class LisaWheeler
    {
        public int LisaWheelerID { get; set; }

        [Required(ErrorMessage = "Your first name is required.")]
        [MaxLength(50, ErrorMessage = "Your first name is seriously longer than 50 characters? Call us, sorry :(")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Your last name is required.")]
        [MaxLength(50, ErrorMessage = "Jesus Christ you have a long last name! It broke the system!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        
        [Required(ErrorMessage = "Gotta tell us where you live! So we can bill you. Sorry, fact of life.")]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "You know, I suppose you can technically live in a town or village, but we need something resembling a group of people.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Sorry, I know you're used to having a box, but I'm unpaid. I seriously need your state though.")]
        [Display(Name = "State (two letters please!)")]
        [MaxLength(2, ErrorMessage = "You know, the standard postal service abbreviations. Yours is probably 'FL'")]
        public string State { get; set; }

        [Required(ErrorMessage = "In the future, we're just going to ask for your address and zip code, then look up your city. But that's not today.")]
        [MaxLength(5)]
        [Display(Name = "Zip Code")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "We'll default to email if something goes wrong, but we need your phone number juuuuuust in case.")]
        [MaxLength(15, ErrorMessage = "Just the digits! Please and thank you!")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "This is how we'll contact you! Don't you want to find out who is critiquing you??")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Are you an SCBWI member?")]
        public string Member { get; set; }

        [Display(Name = "Do you want a manuscript critique?")]
        public string Critique { get; set; }

        public string Critiquer { get; set; }

        public bool WaitingList { get; set; }

        public string PayPalID { get; set; }
        public string Account { get; set; }

        public double Total { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Paid { get; set; }
        public DateTime? Cleared { get; set; }

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
    }
}