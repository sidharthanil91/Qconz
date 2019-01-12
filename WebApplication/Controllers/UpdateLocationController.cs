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
        public HttpResponseMessage Post(JArray jsonResult)
        {
            var x = jsonResult;
            string token = null;
            var searchdetails = "";

            if (HttpContext.Current.Request.Headers.Get("authToken") != null)
            {
                token = Convert.ToString(HttpContext.Current.Request.Headers.Get("authToken"));
            }

            var y = _ILoginService.ValidateToken(token.Replace(' ', '+'));
            if (y != null)
            {
                int userid = y.UserId;
                //int userid = 2;
                string message = "";

                foreach (JObject item in jsonResult.Children())
                {
                    var itemProperties = item.Children<JProperty>();
                    var LatitudeElement = itemProperties.FirstOrDefault(z => z.Name == "latitude");
                    var latitude = LatitudeElement.Value;
                    var longitudeElement = itemProperties.FirstOrDefault(z => z.Name == "longitude");
                    var longitude = longitudeElement.Value;
                    var DateTimeElement = itemProperties.FirstOrDefault(z => z.Name == "timestamp");
                    string datetime1 = Convert.ToString(DateTimeElement.Value);

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
                    searchdetails += "\"Status\":1,\"Message\":\"Success\",";
                    searchdetails += "\"Content\":\"Success\"";
                    //searchdetails = searchdetails + "[{onlineStatus:" + "" + ",onlineStatusChangedTime:";
                    //searchdetails += "}],";
                    searchdetails += ",\"ErrorCode\":0";
                    searchdetails += "}";
                }
                else
                {
                    searchdetails += "{";
                    searchdetails += "\"Status\":0,\"Message\":\"Failed\",";
                    searchdetails += "\"Content\":\"Failed\"";
                    //searchdetails = searchdetails + "[{}]";
                    searchdetails += "\"ErrorCode\":1";
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
                searchdetails += "\"Status\":0,\"Message\":\"Login Failed\",";
                searchdetails += "\"Content\":\"Failed\"";
                //searchdetails = searchdetails + "[{}]";
                searchdetails += ",\"ErrorCode\":1";
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
