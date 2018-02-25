using QconzLocate.Models;
using QconzLocateService.ApiQconzLocateInterface;
using QconzLocateService.ApiQconzLocateService;
using QconzLocateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QconzLocate.Controllers
{
    public class EmergencyController : ApiController
    {
        private IUserLocationService _IUserLocationService = new UserLocationService();
        private ILoginService _ILoginService = new LoginService();
        // GET: api/Emergency
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Emergency/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        // POST: api/Emergency
        public ApiResponseViewModel Post(UserLocation location)
        {
            var y = _ILoginService.ValidateToken(location.token.Replace(' ', '+'));
            if (y != null)
            {
                var UserLog = new List<UserLocationServiceModel>();
                UserLog.Add(new UserLocationServiceModel()
                {
                    DateTime = location.date_time,
                    Latitude = location.latitude,
                    Longitude = location.longitude,
                    UserId = y.UserId
                });
                _IUserLocationService.SaveEmergencyLocation(UserLog);
                return new ApiResponseViewModel() { message = "Success", status = "1" };
            }
            return new ApiResponseViewModel() { message = "Failed", status = "0" }; ;
        }

        // PUT: api/Emergency/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Emergency/5
        public void Delete(int id)
        {
        }
    }
}
