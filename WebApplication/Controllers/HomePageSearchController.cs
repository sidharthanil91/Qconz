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
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;

namespace QconzLocate.Controllers
{
    public class HomePageSearchController : ApiController
    {
        private IUserService _IUserService = new UserService();
        private ILoginService _ILoginService = new LoginService();
        private ILocationService _ILocationService = new LocationService();

        [HttpPost]
        public HttpResponseMessage Post(JObject jsonResult)
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
                var c = _IUserService.GetUserDetails(userid);
                var lastlocationtime = _ILocationService.GetUserLastLocationTime(userid);
                if (c != null)
                {
                    searchdetails += "{";
                    searchdetails += "\"Status\":1,\"Message\":\"Success\",";
                    searchdetails += "\"Content\":";
                    searchdetails = searchdetails + "[{\"UserId\":" + userid + ",\"FirstName\":" + "\"" + c.FirstName + "\"" + ",\"LastName\":" + "\"" +
                        c.SurName + "\"" + ",\"Latitude\":" + "\"" + c.BaseLatitude + "\"" + ",\"Longitude\":" + "\"" + c.BaseLongitude + "\"" + ",\"PhoneNumber\":" + "\"" +
                        c.Cellphone + "\"" + ",\"LastLocationTime\":" + "\"" + lastlocationtime + "\"";
                    searchdetails += "}],";
                    searchdetails += "\"ErrorCode\":0";
                    searchdetails += "}";
                }
                else
                {
                    searchdetails += "{";
                    searchdetails += "\"Status\":0,\"Message\":\"Failed\",";
                    searchdetails += "\"Content\":";
                    searchdetails = searchdetails + "[{}]";
                    searchdetails += ",\"ErrorCode\":1";
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
                searchdetails += "\"Status\":0,\"Message\":\"Failed\",";
                searchdetails += "\"Content\":";
                searchdetails = searchdetails + "[{}]";
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
