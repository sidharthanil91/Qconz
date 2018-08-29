
using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateInterface
{
    public interface ILocationService
    {
         List<LocationServiceModel> GetCustomerLocation(int CompanyId, int Customer, int GroupId, int[] ShowMap, int[] ShowPin, int[] ShowBase);
         List<LocationServiceModel> GetUserLocation(int CompanyId, int  UserId, int GroupId,int [] ShowMap,int[] ShowPin, int[] ShowBase);
        List<LocationServiceModel> GetHistoryLocation(int CompanyId, int UserId, DateTime? StartDate, DateTime? EndDate,string Mode);
    }
}
