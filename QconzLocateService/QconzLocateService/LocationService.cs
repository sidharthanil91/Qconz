using QconzLocateDAL.QConzRepository;
using QconzLocateDAL.QConzRepositoryInterface;
using QconzLocateService.Models;
using QconzLocateService.QconzLocateInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.QconzLocateService
{
    public class LocationService:ILocationService
    {
        private ILocationRepository _ILocationRepository = new LocationRepository();
        public List<LocationServiceModel> GetCustomerLocation(int CompanyId,string Customer,int UserId,int GroupId)
        {
            var y = _ILocationRepository.GetCustomerLocation(CompanyId, Customer,UserId, GroupId);
            List<LocationServiceModel> locations = y.Select(t => new LocationServiceModel
            {
                UserId=t.UserId,
                Address = t.Address,
                Name=t.Name,
                Lat = t.Lat,
                Lng = t.Lng,
                Type=t.Type
            }).ToList();
            return locations;
        }

        public List<LocationServiceModel> GetUserLocation(int CompanyId, int UserId, int GroupId)
        {
            var y = _ILocationRepository.GetUserLocation(CompanyId, UserId, GroupId);
            List<LocationServiceModel> locations = y.Select(t => new LocationServiceModel
            {
                UserId=t.UserId,
                Address = t.Address,
                Name=t.Name,
                Lat = t.Lat,
                Lng = t.Lng
            }).ToList();
            return locations;
        }

        public List<LocationServiceModel> GetHistoryLocation(int CompanyId, int UserId,DateTime? Date)
        {
            var y = _ILocationRepository.GetHistoryLocation(CompanyId, UserId, Date);
            List<LocationServiceModel> locations = y.Select(t => new LocationServiceModel
            {
                Name=t.Name,
                Address = t.Address,
                Lat = t.Lat,
                Lng = t.Lng
            }).ToList();
            return locations;
        }
    }
}
