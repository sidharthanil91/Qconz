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
    [RoutePrefix("api/UserLocation")]
    public class UserLocationController : ApiController
    {
        private ILoginService _ILoginService = new LoginService();
        private IUserLocationService _IUserLocationService = new UserLocationService();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("Bulk")]
        // POST api/<controller>
        public ApiResponseViewModel Post([FromBody]ApiUserLocationViewModel location)
        {
            var y = _ILoginService.ValidateToken(location.token.Replace(' ', '+'));
            if (y != null)
            {
                var UserLog = new List<UserLocationServiceModel>();
                UserLog=location.locations.Select(c=>new UserLocationServiceModel()
                {
                    DateTime = c.date_time,
                    Latitude = c.latitude,
                    Longitude = c.longitude,
                    UserId = y.UserId
                }).ToList();
                _IUserLocationService.SaveUserLocation(UserLog);
                return new ApiResponseViewModel() { message = "Success", status = "1" };
            }
            return new ApiResponseViewModel() { message = "Failed", status = "0" }; ;
        }
        [HttpPost]
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
                _IUserLocationService.SaveUserLocation(UserLog);
                return new ApiResponseViewModel() { message = "Success", status = "1" };
            }
            return new ApiResponseViewModel() { message = "Failed", status = "0" }; 
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}