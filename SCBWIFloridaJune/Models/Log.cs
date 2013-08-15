using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models
{
    public class Log
    {
        public int LogID { get; set; }
        public string Origin { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}