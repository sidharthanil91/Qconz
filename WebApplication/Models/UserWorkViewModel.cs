using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class UserWorkViewModel
    {
        public List<string> days { get; set; }
        public List<OverRideViewModel> overrides { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
    public class OverRideViewModel
    {
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
    }
}