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
    public class TeamController : Controller
    {
        
        private ITeamService _ITeamService = new TeamService();
      
        // GET: Team
        public ActionResult TeamReport()
        {
            
            var TeamList = _ITeamService.GetAllTeam().Select(c => new TeamViewModelList
            {
                Id = c.Id,
                CompanyId = c.CompanyId,
                TeamCreatedDate = c.TeamCreatedDate,
                Teamdesc = c.Teamdesc,
                TeamName = c.TeamName,
                TeamStatus = c.TeamStatus
            }).ToList();
            return View("Team", TeamList);
        }

        //public ActionResult CompanyDetails(int id)
        //{
        //    CompanyViewModel CompanyDetails;
        //    if (id != 0)
        //    {
        //        var y = _ICompanyService.GetCompanyDetails(id);
        //        CompanyDetails = new CompanyViewModel
        //        {
        //            Id = y.Id,
        //            Address1 = y.Address1,
        //            Address2 = y.Address2,
        //            ContactName = y.ContactName,
        //            Email = y.Email,
        //            Lat = y.Lat,
        //            Lng = y.Lng,
        //            Phone1 = y.Phone1,
        //            Phone2 = y.Phone2,
        //            Title = y.Title,
        //            Website = y.Website,
        //            ZipCode = y.ZipCode
        //        };
        //        return View("CompanyDetails", CompanyDetails);
        //    }
        //    else
        //    {
        //        CompanyDetails = new CompanyViewModel
        //        {
        //            Id = 0,
        //            Address1 = null,
        //            Address2 = null,
        //            ContactName = null,
        //            Email = null,
        //            Lat = null,
        //            Lng = null,
        //            Phone1 = null,
        //            Phone2 = null,
        //            Title = null,
        //            Website = null,
        //            ZipCode = null
        //        };
        //        return View("CompanyDetails", CompanyDetails);
        //    }
        //}

        //[HttpPost]
        //public JsonResult SaveDetails(CompanyViewModel company)
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