using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models.ViewModels
{
    public class MealModel
    {
        [Required]
        [DisplayName("Meal One")]
        public string Meal1 { get; set; }

        [Required]
        [DisplayName("Meal Two")]
        public string Meal2 { get; set; }

        [Required]
        [DisplayName("Meal Three")]
        public string Meal3 { get; set; }
    }
}