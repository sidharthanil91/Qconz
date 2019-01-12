using QconzLocateService.ApiQconzLocateInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QconzLocateService.Models;
using QconzLocateDAL.ApiQconzRepositoryInterface;
using QconzLocateDAL.ApiQconzRepository;
using QconzLocateDAL.QConzRepositoryModel;

namespace QconzLocateService.ApiQconzLocateService
{
    public class UserLocationService : IUserLocationService
    {
        private IUserLocationRepository _IUserLocationRepository = new UserLocationRepository();

        public void SaveEmergencyLocation(List<UserLocationServiceModel> UserLocation)
        {
            var UserLog = new List<UserLocationModel>();
            UserLog = UserLocation.Select(c => new UserLocationModel
            {
                DateTime = c.DateTime,
                Longitude = c.Longitude,
                Latitude = c.Latitude,
                UserId = c.UserId
            }).ToList();
            _IUserLocationRepository.SaveUserEmergencyLocation(UserLog);
        }

        public void SaveUserLocation(List<UserLocationServiceModel> UserLocation)
        {
            var UserLog = new List<UserLocationModel>();
            UserLog=UserLocation.Select(c=> new UserLocationModel
            {
                DateTime = c.DateTime,
                Longitude = c.Longitude,
                Latitude = c.Latitude,
                UserId = c.UserId
            }).ToList();
            _IUserLocationRepository.SaveUserLocation(UserLog);
        }

        public string SaveUserLocationDetails(UserLocationServiceModel UserLocationDetails)
        {
            var UserLog = new UserLocationModel()
            {
                DateTime = UserLocationDetails.DateTime,
                Longitude = UserLocationDetails.Longitude,
                Latitude = UserLocationDetails.Latitude,
                UserId = UserLocationDetails.UserId
            };
            return _IUserLocationRepository.SaveUserLocationDetails(UserLog);

        }
    }
}
