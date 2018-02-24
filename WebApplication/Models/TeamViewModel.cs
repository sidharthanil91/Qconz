using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class TeamViewModel
    {
        public List<TeamViewModelList> TeamList { get; set; }
        public TeamViewModelList SingleTeam { get; set; }
        public List<SelectListItems> GroupList { get; set; }
        public List<SelectListItems> CompanyList { get; set; }
        public List<SelectListItems> UserList { get; set; }

    }
    public class TeamViewModelList
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string Teamdesc { get; set; }
        public string UserId { get; set; }
        public DateTime? TeamCreatedDate { get; set; }
        public string TeamStatus { get; set; }
        public string TeamStatusId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
    public class SelectListItems
    {
        public int id { get; set; }
        public string text { get; set; }
    }
}