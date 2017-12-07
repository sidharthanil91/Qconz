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
    public class RosterController : Controller
    {
        // GET: Roster
        private IRosterService _IRosterService = new RosterService();
        private List<RosterListViewModel> RosterList = new List<RosterListViewModel>();
      
        // GET: Company
        public ActionResult RosterReport()
        {
            RosterViewModel rosters = new RosterViewModel();
            RosterList = _IRosterService.GetAllRoster().Select(c => new RosterListViewModel
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
            RosterViewModel RosterDetails=new RosterViewModel();
            if (id != 0)
            {
                var c = _IRosterService.GetRosterDetails(id);
                RosterDetails.Roster = new RosterListViewModel
                {
                    Id = c.Id,
                    EndDate = c.EndDate,
                    FinishTime = c.FinishTime,
                    StartDate = c.StartDate,
                    StartTime = c.StartTime,
                    UserId = c.UserId
                };
                return View("RosterDetails", RosterDetails);
            }
            else
            {
                RosterDetails.Roster = new RosterListViewModel
                {
                    Id = 0,
                    EndDate = null,
                    //FinishTime = ,
                    //StartDate = null,
                    //StartTime = c.StartTime,
                    //UserId = c.UserId
                };
                return View("RosterDetails", RosterDetails);
            }
        }

        //[HttpPost]
        //public JsonResult SaveDetails(RosterListViewModel company)
        //{
        //    CompanyServiceModel CompanyModel;
        //    CompanyModel = new CompanyServiceModel()
        //    {
        //        Id = company.Id,
        //        Address1 = company.Address1,
        //        Address2 = company.Address2,
        //        ContactName = company.ContactName,
        //        Email = company.Email,
        //        Lat = company.Lat,
        //        Lng = company.Lng,
        //        Phone1 = company.Phone1,
        //        Phone2 = company.Phone2,
        //        Title = company.Title,
        //        Website = company.Website,
        //        ZipCode = company.ZipCode
        //    };
        //    _ICompanyService.SaveCompanyDetails(CompanyModel);
        //    bool success = true;
        //    return Json(success, JsonRequestBehavior.AllowGet);
        //}
    }
}