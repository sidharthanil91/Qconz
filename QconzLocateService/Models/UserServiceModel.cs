using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.Models
{
    public class UserServiceModel
    {
        public int Id { get; set; }
        public int UserType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserStatus { get; set; }
        public string UserGroups { get; set; }
        public Nullable<int> UserTeamId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string Company { get; set; }
        public string UserToken { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string WorkingDays { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyContactNo { get; set; }
        public string BaseLatitude { get; set; }
        public string BaseLongitude { get; set; }
        public string IsContractor { get; set; }
        public int? DefaultGroup { get; set; }
    }
}
