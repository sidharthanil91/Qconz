using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class ApiLoginViewModel
    {
        public string token { get; set; }
        public string status { get; set; }
        public DateTime date { get; set; }
        public string message { get; set; }
        public int onlineStatus { get; set; }
        public string onlineStatusChangedTime { get; set; }
    }
}