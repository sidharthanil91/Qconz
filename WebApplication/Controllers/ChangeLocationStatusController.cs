using Newtonsoft.Json.Linq;
using QconzLocate.Models;
using QconzLocateService.ApiQconzLocateInterface;
using QconzLocateService.ApiQconzLocateService;
using QconzLocateService.Models;
using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;

namespace QconzLocate.Controllers
{
    public class ChangeLocationStatusController : ApiController
    {
        private IUserService _IUserService = new UserService();
        private ILoginService _ILoginService = new LoginService();
        private IStopTrackingService _IStopTrackingService = new StopTrackingService();

        [HttpPost]
        public HttpResponseMessage Post(JObject jsonResult)
        {
            var x = jsonResult;
            string token = null;
            var searchdetails = "";
            int onlineStatus = 0;

            onlineStatus = (int)x.SelectToken("timeInterval");
            token = (string)x.SelectToken("token");
            //if (HttpContext.Current.Request.Headers.Get("authToken") != null)
            //{
            //    token = Convert.ToString(HttpContext.Current.Request.Headers.Get("authToken"));
            //}

            var y = _ILoginService.ValidateToken(token.Replace(' ', '+'));
            if (y != null)
            {
                int userid = y.UserId;
                //int userid = 2;
                var user = _IUserService.GetUserDetails(userid);
                var currentDateTime = DateTime.UtcNow;
                string message1 = null;

                UserServiceModel UserModel;
                UserModel = new UserServiceModel()
                {
                    Id = user.Id,
                    Cellphone = user.Cellphone,
                    CompanyId = user.CompanyId,
                    EmergencyContact = user.EmergencyContact,
                    EmergencyContactNo = user.EmergencyContactNo,
                    Email = user.Email,
                    EndTime = user.EndTime,
                    FirstName = user.FirstName,
                    Password = user.Password,
                    StartTime = user.StartTime,
                    SurName = user.SurName,
                    UserName = user.UserName,
                    UserGroups = user.UserGroups,
                    UserStatus = user.UserStatus,
                    UserTeamId = user.UserTeamId,
                    UserToken = user.UserToken,
                    UserType = user.UserType,
                    WorkingDays = user.WorkingDays,
                    BaseLatitude = user.BaseLatitude,
                    BaseLongitude = user.BaseLongitude,
                    DefaultGroup = user.DefaultGroup,
                    IsContractor = user.IsContractor,
                    OnlineStatus = onlineStatus,
                    OnlineStatusChangeTime = currentDateTime,
                };
                var message = _IUserService.SaveUserDetails(UserModel);
                if (message == null)
                {
                    StopTrackingServiceModel StopTrackingModel;
                    StopTrackingModel = new StopTrackingServiceModel()
                    {
                        Id = 0,
                        UserId = user.Id,
                        Hours = 0,
                        Status = Convert.ToString(onlineStatus),
                        DateTime = currentDateTime,
                        User = null,
                    };
                    message1 = _IStopTrackingService.SaveStopTrackingDetails(StopTrackingModel);
                }
                if (message == null && message1 == null)
                {
                    
                    var OnlineStatusChangedTime = currentDateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", DateTimeFormatInfo.InvariantInfo);

                    searchdetails += "{";
                    searchdetails += "\"status\":1,\"message\":\"Success\",";
                    searchdetails += "\"content\":";
                    searchdetails = searchdetails + "[{\"onlineStatus\":" + onlineStatus + ",\"onlineStatusChangedTime\":" + "\"" + OnlineStatusChangedTime + "\"";
                    searchdetails += "}],";
                    searchdetails += "\"errorCode\":0";
                    searchdetails += "}";
                }
                else
                {
                    searchdetails += "{";
                    searchdetails += "\"status\":0,\"message\":\"Failed\",";
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
                return resp;

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
                return resp;

                //return Request.CreateResponse(HttpStatusCode.OK, searchdetails, Configuration.Formatters.JsonFormatter);
            }
        }

    }
}
