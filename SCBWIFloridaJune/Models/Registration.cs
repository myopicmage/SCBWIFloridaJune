using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models
{
    public class Registration
    {
        public int RegistrationID { get; set; }

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
    }
}