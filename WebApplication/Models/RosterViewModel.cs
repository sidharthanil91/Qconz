using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class RosterViewModel
    {
        public List<RosterListViewModel> RosterList { get; set; }
        public RosterListViewModel Roster { get; set; }
        public List<SelectListItems> UserList { get; set; }
        public List<SelectListItems> GroupList { get; set; }
    }

    public class RosterListViewModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string UserId { get; set; }
        public string TeamId { get; set; }
        public string Status { get; set; }
        public string Override { get; set; }
        public string OverrideDetails { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
    }

    public class RosterIndividual
    {
        public string startDateTime { get; set; }
        public string endDateTime { get; set; }
    }
}