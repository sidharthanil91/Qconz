using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class ApiCredentialsViewModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string deviceType { get; set; }
        public string deviceId { get; set; }
    }
}