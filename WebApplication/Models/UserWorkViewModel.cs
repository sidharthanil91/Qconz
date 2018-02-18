using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class UserWorkViewModel
    {
        public RosterIndividual roster { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}