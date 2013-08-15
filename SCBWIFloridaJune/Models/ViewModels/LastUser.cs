using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models.ViewModels
{
    public class LastUser
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Creation Date")]
        public DateTime Created { get; set; }
    }
}