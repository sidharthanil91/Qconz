using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryModel
{
    public class StopTrackingModel
    {
        public int? Hours { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
    }
}
