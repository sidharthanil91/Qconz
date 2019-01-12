using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class ApiHomePageSearchViewModel
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int usertype { get; set; }
        public string token { get; set; }
    }
}