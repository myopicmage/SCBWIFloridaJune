using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models
{
    public class Critique
    {
        public int CritiqueID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string Type { get; set; }
    }
}