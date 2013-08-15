using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models.ViewModels
{
    public class TrackModel
    {
        [Required]
        [DisplayName("Track 1 Name")]
        public string Track1 { get; set; }

        [DisplayName("Track 1 Presenters")]
        public string Track1Presenters { get; set; }

        [Required]
        [DisplayName("Track 2 Name")]
        public string Track2 { get; set; }

        [DisplayName("Track 2 Presenters")]
        public string Track2Presenters { get; set; }

        [Required]
        [DisplayName("Track 3 Name")]
        public string Track3 { get; set; }

        [DisplayName("Track 3 Presenters")]
        public string Track3Presenters { get; set; }

        [Required]
        [DisplayName("Track 4 Name")]
        public string Track4 { get; set; }

        [DisplayName("Track 4 Presenters")]
        public string Track4Presenters { get; set; }

        [Required]
        [DisplayName("Track 5 Name")]
        public string Track5 { get; set; }

        [DisplayName("Track 5 Presenters")]
        public string Track5Presenters { get; set; }
    }
}