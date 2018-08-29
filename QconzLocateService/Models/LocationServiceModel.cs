using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.Models
{
    public class LocationServiceModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Type { get; set; }
        public bool ShowPin { get; set; }
    }
}
