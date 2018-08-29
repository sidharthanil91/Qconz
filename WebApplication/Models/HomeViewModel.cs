using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class HomeViewModel
    {
        public int UserId { get; set; }
        public List<HistoryGridModel>HistoryGrid { get; set; }
        public List<SelectListItems> UserLists { get; set; }
        public List<SelectListItems> GroupLists { get; set; }
        public List<SelectListItems> CustomerLists { get; set; }
        public List<TeamDetailsModel> TeamDetails { get; set; }
    }
    public class HistoryGridModel
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public string Date { get; set; }
    }
    public class TeamDetailsModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public bool StopTracking { get; set; }
        public bool ShowOnMap { get; set; }
        public bool ShowPin { get; set; }
        public bool BaseDetail { get; set; }
    }
}