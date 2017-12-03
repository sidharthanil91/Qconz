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
    [Authorize(Roles = "SUPER,ADMIN")]
    public class TeamController : Controller
    {
        
        private ITeamService _ITeamService = new TeamService();
        private CommonService _commonService = new CommonService();
        
        // GET: Team
        public ActionResult TeamReport()
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var TeamList = _ITeamService.GetAllTeam(CompanyId).Select(c => new TeamViewModelList
            {
                Id = c.Id,
                CompanyId = c.CompanyId,
                TeamCreatedDate = c.TeamCreatedDate,
                Teamdesc = c.Teamdesc,
                TeamName = c.TeamName,
                TeamStatus = c.TeamStatus,
                CompanyName=c.CompanyName
            }).ToList();
            return View("Team", TeamList);
        }

        public ActionResult TeamDetails(int id)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            TeamViewModel TeamViewModel = new TeamViewModel();
            List<SelectListItems> companyselect = new List<SelectListItems>();
            TeamViewModelList TeamDetails;
            if (id != 0)
            {
                var c = _ITeamService.GetTeamDetails(id);
                TeamDetails = new TeamViewModelList
                {
                    Id = c.Id,
                    CompanyId = c.CompanyId,
                    TeamCreatedDate = c.TeamCreatedDate,
                    Teamdesc = c.Teamdesc,
                    TeamName = c.TeamName,
                    TeamStatus = c.TeamStatus
                };
                TeamViewModel.SingleTeam = TeamDetails;
                var y = _commonService.GetCompanySelectList(CompanyId);
                companyselect = y.CompanyList.Select(t=>new SelectListItems {
                    id=t.Id,
                    text=t.Text
                }).ToList();
                TeamViewModel.CompanyList = companyselect;
                return View("TeamDetails", TeamViewModel);
            }
            else
            {
                TeamDetails = new TeamViewModelList
                {
                    Id = 0,
                    CompanyId =0,
                    TeamCreatedDate = null,
                    Teamdesc = null,
                    TeamName = null,
                    TeamStatus = null
                };
                return View("TeamDetails", TeamDetails);
            }
        }

        [HttpPost]
        public JsonResult SaveDetails(TeamViewModelList team)
        {
            TeamServiceModel TeamModel;
            TeamModel = new TeamServiceModel()
            {
                Id = team.Id,
                CompanyId = team.CompanyId,
                TeamCreatedDate = team.TeamCreatedDate,
                Teamdesc = team.Teamdesc,
                TeamName = team.TeamName,
                TeamStatus = team.TeamStatus
            };
            _ITeamService.SaveTeamDetails(TeamModel);
            bool success = true;
            return Json(success, JsonRequestBehavior.AllowGet);
        }
    }
}