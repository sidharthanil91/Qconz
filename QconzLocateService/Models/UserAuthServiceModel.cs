using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.Models
{
    public class UserAuthServiceModel
    {
        public string Email { get; set; }
        public string Image { get; set; }
        public string Roles { get; set; }
        public string Password { get; set; }
        public int? CompanyId { get; set; }
    }
}

