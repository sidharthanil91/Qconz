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
        public List<LocationServiceModel> GetCustomerLocation(int CompanyId,int Customer,int GroupId, int[] ShowMap, int[] ShowPin, int[] ShowBase)
        {
            var y = _ILocationRepository.GetCustomerLocation(CompanyId, Customer, GroupId, ShowMap,  ShowPin,ShowBase);
            List<LocationServiceModel> locations = y.Select(t => new LocationServiceModel
            {
                UserId=t.UserId,
                Address = t.Address,
                Contact=t.Contact,
                Name=t.Name,
                Lat = t.Lat,
                Lng = t.Lng,
                Type=t.Type,
                ShowPin = t.ShowPin
            }).ToList();
            return locations;
        }

        public List<LocationServiceModel> GetUserLocation(int CompanyId, int UserId, int GroupId, int[] ShowMap, int[] ShowPin, int[] ShowBase)
        {
            var y = _ILocationRepository.GetUserLocation(CompanyId, UserId, GroupId,ShowMap,ShowPin,ShowBase);
            List<LocationServiceModel> locations = y.Select(t => new LocationServiceModel
            {
                UserId=t.UserId,
                Address = t.Address,
                Name=t.Name,
                Lat = t.Lat,
                Lng = t.Lng,
                ShowPin=t.ShowPin
            }).ToList();
            return locations;
        }

        public List<LocationServiceModel> GetHistoryLocation(int CompanyId, int UserId,DateTime? StartDate, DateTime? EndDate,string Mode)
        {
            var y = _ILocationRepository.GetHistoryLocation(CompanyId, UserId, StartDate, EndDate,Mode);
            List<LocationServiceModel> locations = y.Select(t => new LocationServiceModel
            {
                UserId=t.UserId,
                Name=t.Name,
                Address = t.Address,
                Lat = t.Lat,
                Lng = t.Lng
            }).ToList();
            return locations;
        }
    }
}
