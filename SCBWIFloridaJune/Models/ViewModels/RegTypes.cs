using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models.ViewModels
{
    public class RegTypes
    {
        [Required]
        [DisplayName("Early Member")]
        public double EarlyMemberPrice { get; set; }

        [Required]
        [DisplayName("Late Member")]
        public double LateMemberPrice { get; set; }

        [Required]
        [DisplayName("Early Non Member")]
        public double EarlyNonMemberPrice { get; set; }

        [Required]
        [DisplayName("LateNonMember")]
        public double LateNonMemberPrice { get; set; }
    }
}