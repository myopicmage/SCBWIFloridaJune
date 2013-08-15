using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models.ViewModels
{
    public class OpenCloseDate
    {
        [Required]
        [DisplayName("Conference Start date")]
        public DateTime OpenDate { get; set; }

        [Required]
        [DisplayName("Conference End date")]
        public DateTime CloseDate { get; set; }

        [Required]
        [DisplayName("Registration Open Date")]
        public DateTime RegOpenDate { get; set; }

        [Required]
        [DisplayName("Registration Close Date")]
        public DateTime RegCloseDate { get; set; }

        [Required]
        [DisplayName("Late Registration Pricing Start Date")]
        public DateTime RegLateDate { get; set; }

        [Required]
        [DisplayName("Final Date For Users To Edit Their Information")]
        public DateTime LastEditDate { get; set; }
    }
}