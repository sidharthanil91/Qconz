﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.Models
{
   public class TeamServiceModel
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string Teamdesc { get; set; }
        public DateTime? TeamCreatedDate { get; set; }
        public string TeamStatus { get; set; }
        public int CompanyId { get; set; }
    }
}
