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
    public class UserWorkController : ApiController
    {
        private ILoginService _IloginService =new LoginService();
        private IUserWorkService _IUserWorkService = new UserWorkService();
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
        public UserWorkViewModel Post([FromBody] ApiLoginViewModel tokendata)
        {
            var y = _IloginService.ValidateToken(tokendata.token.Replace(' ', '+'));
            if(  y!=null)
            {
                var RosterDetails = _IUserWorkService.GetUserWorkRoster(y.UserId,tokendata.date);
         
                return new UserWorkViewModel() {days=RosterDetails.Days,overrides=RosterDetails.OverRides.Select(t=>new OverRideViewModel {start_date=t.StartDate,end_date=t.EndDate,start_time=t.StartTime,end_time=t.FinishTime }).ToList(), start_time = RosterDetails.StartTime,end_time=RosterDetails.FinishTime , message = "Success", status = "1", };
               
            }
            return new UserWorkViewModel() { message = "Failed", status = "0"};
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