using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models
{
    public class Date
    {
        public int DateID { get; set; }

        [Required(ErrorMessage = "You must enter a name for your date")]
        [DisplayName("Date Name")]
        [MaxLength(30, ErrorMessage = "Please don't name the name too long!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A date is actually required.")]
        [DisplayName("Date")]
        [DataType(DataType.DateTime)]
        public DateTime Value { get; set; }

        [Required(ErrorMessage = "Select a category.")]
        public Category Category { get; set; }
    }
}