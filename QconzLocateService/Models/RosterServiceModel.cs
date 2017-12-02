﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.Models
{
    public class RosterServiceModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan FinishTime { get; set; }
    }
}
