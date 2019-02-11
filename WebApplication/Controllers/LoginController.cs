using Newtonsoft.Json.Linq;
using QconzLocate.Models;
using QconzLocateService.ApiQconzLocateInterface;
using QconzLocateService.ApiQconzLocateService;
using QconzLocateService.Models;
using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Globalization;

namespace QconzLocate.Controllers
{
    public class LoginController : ApiController
    {
        private ILoginService _ILoginService = new LoginService();
        private IUserService _IUserService = new UserService();

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
        //public ApiLoginViewModel Post([FromBody]ApiCredentialsViewModel login)
        //{
        //    LoginServiceModel LoginDetails = new LoginServiceModel()
        //    {
        //        UserName = login.email,
        //        Password = login.password,
        //        DeviceId=login.deviceId,
        //        DeviceType=login.deviceType
        //    };
        //    var y = _ILoginService.ValidateUser(LoginDetails);
        //    ApiLoginViewModel Login = new ApiLoginViewModel()
        //    {
        //        status = y.Status,
        //        message = y.Message,
        //        //onlineStatus=y.

        //    };
        //    System.Web.HttpContext.Current.Response.Headers.Add("authToken", y.Token);
        //    return Login;
        //}

        

        [HttpPost]
        public HttpResponseMessage Post(JObject jsonResult)
        {
            var x = jsonResult;
            var searchdetails = "";
            var email = "";
            var password = "";
            var deviceId = "";
            var deviceType = "";

            email = (string)x.SelectToken("email");
            password = (string)x.SelectToken("password");
            deviceId = (string)x.SelectToken("deviceId");
            deviceType = (string)x.SelectToken("deviceType");

            LoginServiceModel LoginDetails = new LoginServiceModel()
            {
                UserName = email,
                Password = password,
                DeviceId = deviceId,
                DeviceType = deviceType
            };
            var y = _ILoginService.ValidateUser(LoginDetails);
            if (y != null && y.Status!="0")
            {
                //int userid = y.UserId;
                //int userid = 2;
                var c = _IUserService.GetUserDetailsByName(email, password);           
                
                if (c != null)
                {
                    DateTime OnlineStatusChangedTime = Convert.ToDateTime(c.OnlineStatusChangeTime);
                    var lastlocationtime = OnlineStatusChangedTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", DateTimeFormatInfo.InvariantInfo);
                    searchdetails += "{";
                    searchdetails += "\"status\":1,\"message\":\"Success\",";
                    searchdetails += "\"content\":";
                    searchdetails = searchdetails + "{\"userId\":" + c.Id + ",\"firstName\":" + "\"" + c.FirstName + "\"" + ",\"lastName\":" + "\"" +
                        c.SurName + "\"" + ",\"userType\":" + c.UserType + ",\"latitude\":" + "\"" + c.BaseLatitude + "\"" + ",\"longitude\":" + "\"" + c.BaseLongitude + "\"" + ",\"phoneNumber\":" + "\"" +
                        c.Cellphone + "\"" + ",\"onlineStatus\":" + "\"" + c.OnlineStatus + "\"" + ",\"onlineStatusChangedTime\":" + "\"" + lastlocationtime + "\"" ;
                    searchdetails += "}";
                    searchdetails += ",\"errorCode\":0";
                    searchdetails += ",\"token\":"+ "\""+y.Token + "\"";
                    searchdetails += "}";
                }
                else
                {
                    searchdetails += "{";
                    searchdetails += "\"status\":0,\"message\":\"User Details not found\",";
                    searchdetails += "\"content\":";
                    searchdetails = searchdetails + "[{}]";
                    searchdetails += ",\"errorCode\":1";
                    searchdetails += "}";
                }

                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent(searchdetails)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //resp.Content.Headers.Add("authToken", y.Token);
                return resp;

                //System.Web.HttpContext.Current.Response.Headers.Add("authToken", y.Token);
                //return Request.CreateResponse(HttpStatusCode.OK, searchdetails, Configuration.Formatters.JsonFormatter);
            }
            else
            {
                searchdetails += "{";
                searchdetails += "\"status\":-1,\"message\":\"Login Failed\",";
                searchdetails += "\"content\":";
                searchdetails = searchdetails + "[{}]";
                searchdetails += ",\"errorCode\":1";
                searchdetails += "}";

                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent(searchdetails)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //resp.Content.Headers.Add("authToken", y.Token);
                return resp;
            }
            //return Request.CreateResponse(HttpStatusCode.OK, searchdetails, Configuration.Formatters.JsonFormatter);
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