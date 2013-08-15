using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models.ViewModels
{
    public class IntensivePrices
    {
        [Required]
        [DisplayName("Intensive Price")]
        public double IntensivePrice { get; set; }

        [Required]
        [DisplayName("Intensive 1 Name")]
        public string Intensive1 { get; set; }

        [Required]
        [DisplayName("Intensive 2 Name")]
        public string Intensive2 { get; set; }

        [Required]
        [DisplayName("Intensive 3 Name")]
        public string Intensive3 { get; set; }

        [Required]
        [DisplayName("Intensive 4 Name")]
        public string Intensive4 { get; set; }

        [Required]
        [DisplayName("Critique Price")]
        public double CritiquePrice { get; set; }

        [Required]
        [DisplayName("How do you want your manuscript critiques labelled to users?")]
        public string AuthorCritique { get; set; }

        [Required]
        [DisplayName("How do you want your art portfolio critiques labelled to users?")]
        public string ArtCritique { get; set; }

        [Required]
        [DisplayName("How would you like your dummy reviews labelled to users?")]
        public string DummyCritique { get; set; }
    }
}