using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models
{
    public class Track
    {
        public int TrackID { get; set; }

        public string Name { get; set; }

        public string Presenters { get; set; }

        public string Display {
            get { return Name + ", Presented by: " + Presenters; }
        }
    }
}