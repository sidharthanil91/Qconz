using QconzLocateService.QconzLocateInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QconzLocateService.Models;
using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateDAL.QConzRepository;

namespace QconzLocateService.QconzLocateService
{
    public class EmergencyCallService: IEmergencyCallService
    {
        private IEmergencyCallRepository _IEmergencyCallRepository = new EmergencyCallRepository();
        public List<EmergencyCallServiceModel> GetEmergencyList(int CompanyId, int UserId)
        {
            var y = _IEmergencyCallRepository.GetEmergencyCall(CompanyId, UserId);
            List<EmergencyCallServiceModel> Emergency = y.Select(t => new EmergencyCallServiceModel
            {
                UserId=t.UserId,
                DateTime = t.DateTime,
                Latitude = t.Latitude,
                Longitude = t.Longitude,
                User = t.User
            }).ToList();
            return Emergency;
        }
    }
}
