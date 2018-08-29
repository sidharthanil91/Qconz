using QconzLocateDAL.QConzRepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateDAL.QConzRepositoryInterface
{
    public interface ILocationRepository
    {
       List<LocationModel> GetCustomerLocation(int CompanyId, int Customer, int GroupId, int[] ShowMap, int[] ShowPin, int[] ShowBase);
       List<LocationModel> GetUserLocation(int CompanyId, int UserId, int GroupId, int[] ShowMap, int[] ShowPin, int[] ShowBase);
        List<LocationModel> GetHistoryLocation(int CompanyId, int UserId, DateTime? StartDate, DateTime? EndDate, string Mode);
    }
}
