using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryModel
{
    public class RosterModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
    }
}
