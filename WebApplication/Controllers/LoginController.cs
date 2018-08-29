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
    public class LoginController : ApiController
    {
        private ILoginService _ILoginService = new LoginService();
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

        // POST api/<controller>
        public ApiLoginViewModel Post([FromBody]ApiCredentialsViewModel login)
        {
            LoginServiceModel LoginDetails = new LoginServiceModel()
            {
                UserName = login.email,
                Password = login.password,
                DeviceId=login.deviceId,
                DeviceType=login.deviceType
            };
            var y = _ILoginService.ValidateUser(LoginDetails);
            ApiLoginViewModel Login = new ApiLoginViewModel()
            {
                status = y.Status,
                message = y.Message,
                //onlineStatus=y.

            };
            System.Web.HttpContext.Current.Response.Headers.Add("authToken", y.Token);
            return Login;
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