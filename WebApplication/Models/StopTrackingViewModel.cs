using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class StopTrackingViewModel
    {
        public List<StopTrackingListViewModel> StopTrackingList { get; set; }
    }
    public class StopTrackingListViewModel
    {
        public int? Hours { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
    }
}