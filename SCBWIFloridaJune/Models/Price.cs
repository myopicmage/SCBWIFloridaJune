using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models
{
    public class Price
    {
        public int PriceID { get; set; }

        [Required(ErrorMessage = "Please set a name for this price.")]
        [DisplayName("Price Name")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "A price needs a value!")]
        [DisplayName("Price (USD)")]
        public double Value { get; set; }

        [Required(ErrorMessage = "Select the appropriate category.")]
        [DisplayName("Category")]
        public Category Category { get; set; }
    }
}