using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateInterface
{
    public interface IStopTrackingService
    {
         List<StopTrackingServiceModel> GetStopTrackingList(int companyId);
    }
}
