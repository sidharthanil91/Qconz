using QconzLocate.Models;
using QconzLocateService.Models;
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
    [Authorize(Roles = "SUPER,ADMIN")]
    public class RosterController : Controller
    {
        // GET: Roster
        private IRosterService _IRosterService = new RosterService();
        private List<RosterListViewModel> RosterList = new List<RosterListViewModel>();
        private CommonService _commonservice = new CommonService();
        // GET: Company
        public ActionResult RosterReport()
        {
            int CompanyId = (int)(Session["CompanyId"]);
            RosterViewModel rosters = new RosterViewModel();
            RosterList = _IRosterService.GetAllRoster(CompanyId).Select(c => new RosterListViewModel
            {
                Id = c.Id,
                EndDate = c.EndDate,
                FinishTime = c.FinishTime,
                StartDate = c.StartDate,
                StartTime = c.StartTime,
                UserId = c.UserId
            }).ToList();
            rosters.RosterList = RosterList;
            return View("Roster", rosters);
        }

        public ActionResult RosterDetails(int id)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            RosterViewModel RosterDetails=new RosterViewModel();
            if (id != 0)
            {
                var c = _IRosterService.GetRosterDetails(id);
                RosterDetails.Roster = new RosterListViewModel
                {
                    Id = c.Id,
                    EndDate = c.EndDate==null?DateTime.Now:c.EndDate,
                    FinishTime = c.FinishTime==null?DateTime.Now:c.FinishTime,
                    StartDate = c.StartDate==null?DateTime.Now:c.StartDate,
                    StartTime = c.StartTime==null?DateTime.Now:c.StartTime,
                    UserId = c.UserId
                };
                
               
            }
            else
            {
                RosterDetails.Roster = new RosterListViewModel
                {
                    Id = 0,
                    EndDate = DateTime.Now,
                    FinishTime = DateTime.Now,
                    StartDate = DateTime.Now,
                    StartTime = DateTime.Now,
                    UserId = null
                };
                
            }
            var y = _commonservice.GetUserSelectList(CompanyId);
            RosterDetails.UserList = y.UserList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            return View("RosterDetails", RosterDetails);
        }

        [HttpPost]
        public JsonResult SaveDetails(RosterListViewModel roster)
        {
            RosterServiceModel RosterModel;
            int CompanyId = (int)(Session["CompanyId"]);
            RosterModel = new RosterServiceModel()
            {
                Id = roster.Id,
                CompanyId = CompanyId,
                StartDate = roster.StartDate == null ? DateTime.Now : roster.StartDate,
                EndDate = roster.EndDate == null ? DateTime.Now : roster.EndDate,
                FinishTime =roster.FinishTime,
                StartTime=roster.StartTime,
                UserId =roster.UserId
            };
            _IRosterService.SaveRosterDetails(RosterModel);
            bool success = true;
            return Json(success, JsonRequestBehavior.AllowGet);
        }
    }
}