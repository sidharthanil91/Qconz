﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QconzLocate.Models
{
    public class RosterViewModel
    {
        public List<RosterListViewModel> RosterList { get; set; }
        public RosterListViewModel Roster { get; set; }
    }

    public class RosterListViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan FinishTime { get; set; }
    }
}