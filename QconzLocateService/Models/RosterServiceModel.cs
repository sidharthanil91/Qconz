using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.Models
{
    public class RosterServiceModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string UserId { get; set; }
        public string TeamId { get; set; }
        public string Status { get; set; }
        public string Override { get; set; }
        public string OverrideDetails { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
    }
    public class OverRideFullServiceModel
    {
        public List<string> Days { get; set; }
        public List<OverRideServiceModel> OverRides { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
    }
    public class OverRideServiceModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
    }
}
