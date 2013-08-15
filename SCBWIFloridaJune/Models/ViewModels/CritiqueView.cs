using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models.ViewModels
{
    public class CritiqueView
    {
        [DisplayName("Number of critiques")]
        [DataType(DataType.Text)]
        [Range(0, 8)]
        public int NumCritiques { get; set; }

        [DisplayName("Type of critique")]
        public string CritiqueType { get; set; }
    }
}