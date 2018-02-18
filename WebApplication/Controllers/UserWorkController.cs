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
                var strDate = RosterDetails.StartTime.ToString();
                DateTime dt;
                DateTime.TryParse(strDate, out dt);
                var endDate = RosterDetails.FinishTime.ToString();
                DateTime dt1;
                DateTime.TryParse(endDate, out dt1);
                RosterIndividual rosters;
                if (RosterDetails.Status == "1")
                {
                    rosters = new RosterIndividual()
                    {
                        startDateTime = dt.ToString("HH:mm:ss"),
                        endDateTime = dt1.ToString("HH:mm:ss"),
                    };
                    return new UserWorkViewModel() { message = "Success", status = "1", roster = rosters };
                }
            }
            return new UserWorkViewModel() { message = "Failed", status = "0", roster = null };
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