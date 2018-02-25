using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QconzLocateService.ApiQconzLocateInterface
{
    public interface IUserLocationService
    {
        void SaveUserLocation(List<UserLocationServiceModel> UserLocation);
        void SaveEmergencyLocation(List<UserLocationServiceModel> UserLocation);
    }
}
