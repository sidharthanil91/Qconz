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
    public class UpdateLocationController : ApiController
    {
        private ILoginService _ILoginService = new LoginService();
        private IUserLocationService _IUserLocationService = new UserLocationService();

        [HttpPost]
        public HttpResponseMessage Post([FromBody]ApiUpdateLocationViewModel location)
        {
            //var x = jsonResult;
            //string token = null;
            var searchdetails = "";

            //token = (string)x.SelectToken("token");
            //if (HttpContext.Current.Request.Headers.Get("authToken") != null)
            //{
            //    token = Convert.ToString(HttpContext.Current.Request.Headers.Get("authToken"));
            //}
            var y = _ILoginService.ValidateToken(location.token.Replace(' ', '+'));
           

            if (y != null)
            {
                int userid = y.UserId;
                //int userid = 2;
                string message = "";

                var UserLog = new List<UserLocationServiceModel>();
                UserLog = location.locations.Select(c => new UserLocationServiceModel()
                {
                    DateTime = c.timestamp,
                    Latitude = c.latitude,
                    Longitude = c.longitude,
                    UserId = y.UserId
                }).ToList();

                foreach (var item in UserLog)
                {
                    //var itemProperties = item.Children<JProperty>();
                    //var LatitudeElement = itemProperties.FirstOrDefault(z => z.Name == "latitude");
                    var latitude = item.Latitude;
                    //var longitudeElement = itemProperties.FirstOrDefault(z => z.Name == "longitude");
                    var longitude = item.Longitude;
                    var DateTimeElement = item.DateTime;
                    string datetime1 = Convert.ToString(DateTimeElement);

                    string date = datetime1.Substring(0, 10);
                    string time = datetime1.Substring(11, 8);
                    string datetime = date + " " + time;
                    //UserLocationServiceModel UserLog;
                    var UserLocationDetails = new UserLocationServiceModel();
                    UserLocationDetails = new UserLocationServiceModel()
                    {
                        DateTime = Convert.ToDateTime(datetime),
                        Latitude = Convert.ToString(latitude),
                        Longitude = Convert.ToString(longitude),
                        UserId = userid
                    };

                    message = _IUserLocationService.SaveUserLocationDetails(UserLocationDetails);
                }

                if (message == null)
                {
                    searchdetails += "{";
                    searchdetails += "\"status\":1,\"message\":\"Success\",";
                    searchdetails += "\"content\":\"Success\"";
                    //searchdetails = searchdetails + "[{onlineStatus:" + "" + ",onlineStatusChangedTime:";
                    //searchdetails += "}],";
                    searchdetails += ",\"errorCode\":0";
                    searchdetails += "}";
                }
                else
                {
                    searchdetails += "{";
                    searchdetails += "\"status\":0,\"message\":\"Failed\",";
                    searchdetails += "\"content\":\"Failed\"";
                    //searchdetails = searchdetails + "[{}]";
                    searchdetails += "\"errorCode\":1";
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
                searchdetails += "\"status\":0,\"message\":\"Login Failed\",";
                searchdetails += "\"content\":\"Failed\"";
                //searchdetails = searchdetails + "[{}]";
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
