
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
    public class StopTrackingController : Controller
    {
        private IStopTrackingService _IStopTrackingService = new StopTrackingService();
        private CommonService _commonService = new CommonService();
        // GET: StopTracking
        public ActionResult StopTracking()
        {
            try
            {
                int CompanyId = (int)(Session["CompanyId"]);
                StopTrackingViewModel StopTrackingList = new StopTrackingViewModel();
                StopTrackingList.StopTrackingList = _IStopTrackingService.GetStopTrackingList(CompanyId).Select(c => new StopTrackingListViewModel
                {
                    UserId = c.UserId,
                    DateTime = c.DateTime,
                    Hours = c.Hours,
                    Status = c.Status,
                    User = c.User
                }).ToList();
                //var y1 = _commonService.GetUserSelectList(CompanyId);
                //EmergencyList.UserList = y1.UserList.Select(t => new SelectListItems
                //{
                //    id = t.Id,
                //    text = t.Text
                //}).ToList();
                return View("StopTracking", StopTrackingList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

