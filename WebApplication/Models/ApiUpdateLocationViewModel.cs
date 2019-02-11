using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class ApiUpdateLocationViewModel
    {
        public string token { get; set; }
        public List<UpdateUserLocation> locations { get; set; }
    }
    public class UpdateUserLocation
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public DateTime timestamp { get; set; }
    }
}