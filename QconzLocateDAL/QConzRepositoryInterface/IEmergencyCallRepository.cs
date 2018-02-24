using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryInterface
{
    public interface IEmergencyCallRepository
    {
        List<EmergencyCallModel> GetEmergencyCall(int CompanyId,  int UserId);
    }
}
