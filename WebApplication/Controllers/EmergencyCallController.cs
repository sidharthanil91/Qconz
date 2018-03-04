using QconzLocate.Models;
using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QconzLocate.Controllers
{
    [SessionExpireFilter]
    [Authorize]
    public class EmergencyCallController : Controller
    {
        private IEmergencyCallService _IEmergencyCallService = new EmergencyCallService();
        private CommonService _commonService = new CommonService();

        private EmergencyCallViewModel EmergencyList = new EmergencyCallViewModel();
        public EmergencyCallController()
        {

        }
        // GET: Company
        public ActionResult EmergencyCall()
        {
            try
            {
                int CompanyId = (int)(Session["CompanyId"]);
                EmergencyCallViewModel companies = new EmergencyCallViewModel();
                EmergencyList.EmergencyCalls = _IEmergencyCallService.GetEmergencyList(CompanyId, 0).Select(c => new EmergencyCallList
                {
                    UserId=c.UserId,
                    DateTime=c.DateTime,
                    Latitude=c.Latitude,
                    Longitude=c.Longitude,
                    User=c.User
                }).ToList();
                var y1 = _commonService.GetUserSelectList(CompanyId);
                EmergencyList.UserList = y1.UserList.Select(t => new SelectListItems
                {
                    id = t.Id,
                    text = t.Text
                }).ToList();
                return View("EmergencyCall", EmergencyList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

      
    }
}