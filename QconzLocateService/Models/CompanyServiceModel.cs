﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.Models
{
    public class CompanyServiceModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContactName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public byte[] Image { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public DateTime AddedDate { get; set; }
        public string Archive { get; set; }
    }
}
