using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryModel
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string Teamdesc { get; set; }
        public string UserId { get; set; }
        public DateTime? TeamCreatedDate { get; set; }
        public string TeamStatus { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
