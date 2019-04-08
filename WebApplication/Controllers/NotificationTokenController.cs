using Newtonsoft.Json.Linq;
using QconzLocate.Models;
using QconzLocateService.ApiQconzLocateInterface;
using QconzLocateService.ApiQconzLocateService;
using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QconzLocate.Controllers
{
    public class NotificationTokenController : ApiController
    {
        private IUserService _IUserService = new UserService();
        private ILoginService _ILoginService = new LoginService();
        private ILocationService _ILocationService = new LocationService();
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
        public ResponseViewModel Post(JObject jsonResult)
        {
            var x = jsonResult;
            string token = null;


            token = (string)x.SelectToken("token");
            string firebaseToken= (string)x.SelectToken("firebaseToken");
            //if (HttpContext.Current.Request.Headers.Get("authToken") != null)
            //{
            //    token = Convert.ToString(HttpContext.Current.Request.Headers.Get("authToken"));
            //}
            var reponse = new ResponseViewModel();
            var y = _ILoginService.ValidateToken(token.Replace(' ', '+'));
            if (y != null)
            {
                int userid = y.UserId;

                var result=_IUserService.SaveNotificationDetails(firebaseToken,userid);
                if(result==null)
                {
                    reponse.Message = "Success";
                    reponse.Success = true;
                    return reponse;
                }
                else 
                {
                    reponse.Message = "Failed";
                    reponse.Success = false;
                    return reponse;
                }
            }
            else
            {
                reponse.Message = "Login Failed";
                reponse.Success = false;
                return reponse;
            }
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