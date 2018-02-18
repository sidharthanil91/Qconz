using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class ApiUserLocationViewModel
    {
        public string token { get; set; }
        public List<UserLocation> locations { get; set; }
    }
    public class UserLocation
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public DateTime date_time { get; set; }
        public string token { get; set; }
    }
}