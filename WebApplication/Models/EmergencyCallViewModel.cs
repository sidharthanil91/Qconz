using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class EmergencyCallViewModel
    {
        public List<EmergencyCallList> EmergencyCalls { get; set; }
        public List<SelectListItems> UserList { get; set; }
    }
    public class EmergencyCallList
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
    }
}