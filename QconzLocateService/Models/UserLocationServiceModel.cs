﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.Models
{
    public class UserLocationServiceModel
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
    }
}
