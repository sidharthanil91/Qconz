using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class TeamViewModel
    {
        List<TeamViewModelList> TeamList { get; set; }
    }
    public class TeamViewModelList
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string Teamdesc { get; set; }
        public DateTime? TeamCreatedDate { get; set; }
        public string TeamStatus { get; set; }
        public int CompanyId { get; set; }
    }
}