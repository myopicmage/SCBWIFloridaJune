using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models
{
    public class Information
    {
        public int InformationID { get; set; }

        [Required]
        [DisplayName("Title this information")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Select a category")]
        public Category Category { get; set; }

        [Required]
        [DisplayName("Enter a value")]
        public string Value { get; set; }
    }
}