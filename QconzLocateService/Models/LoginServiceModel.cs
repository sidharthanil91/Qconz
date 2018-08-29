using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.Models
{
    public class LoginServiceModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DeviceType { get; set; }
        public string DeviceId { get; set; }
    }
}
