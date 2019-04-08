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

            token = (string)x.SelectToken("token");

            //if (HttpContext.Current.Request.Headers.Get("authToken") != null)
            //{
            //    token = Convert.ToString(HttpContext.Current.Request.Headers.Get("authToken"));
            //}

            var y = _ILoginService.ValidateToken(token.Replace(' ', '+'));
            if (y != null)
            {
                int userid = y.UserId;
                var result = _IUserService.GetUserTeamDetails(userid);
                if (result != null && result.Count>0)
                {
                    searchdetails += "{";
                    searchdetails += "\"status\":1,\"message\":\"Success\",";
                    searchdetails += "\"content\":[";
                    foreach (var c in result)
                    {
                        var lastlocationtime = _ILocationService.GetUserLastLocationTime(c.Id);
                       
                        searchdetails = searchdetails + "{\"userId\":" + c.Id + ",\"firstName\":" + "\"" + c.FirstName + "\"" + ",\"lastName\":" + "\"" +
                            c.SurName + "\"" + ",\"latitude\":" + "\"" + c.BaseLatitude + "\"" + ",\"longitude\":" + "\"" + c.BaseLongitude + "\"" + ",\"phoneNumber\":" + "\"" +
                            c.Cellphone + "\"" + ",\"lastLocationTime\":" + "\"" + lastlocationtime + "\"";
                        searchdetails += "},";
                       
                    }
                    searchdetails = searchdetails.Substring(0, searchdetails.Length - 1);
                    searchdetails += "],\"errorCode\":0";
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
                searchdetails += "\"status\":0,\"message\":\"Failed\",";
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
