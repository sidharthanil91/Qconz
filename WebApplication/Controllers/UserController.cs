using QconzLocate.Models;
using QconzLocateService.Models;
using QconzLocateService.QconzLocateService;
using QconzLocateService.QconzLocateInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QconzLocate.Controllers
{
    [Authorize(Roles = "SUPER,ADMIN")]
    public class UserController : Controller
    {
        private IUserService _IUserService = new UserService();
        private UserViewModel UserList = new UserViewModel();
        private CommonService _commonservice = new CommonService();
       
        public UserController()
        {

        }
        // GET: User
        public ActionResult UserReport()
        {
            var user = System.Web.HttpContext.Current.User;
            int CompanyId = (int)(Session["CompanyId"]);
            UserListViewModel users1 = new UserListViewModel();
            Dictionary<int, string> UserRole
                       = new Dictionary<int, string>() {
                         {2,"Admin User"},
                         {3,"Dashboard User"},
                         {4,"App User" } };

            UserList.UserListViewModel = _IUserService.GetAllUsers(CompanyId).Select(c => new UserViewModelList
            {
                Id = c.Id,
                Cellphone = c.Cellphone,
                CompanyId = c.CompanyId,
                EmergencyContact = c.EmergencyContact,
                EmergencyContactNo=c.EmergencyContactNo,
                Email = c.Email,
                EndTime = c.EndTime,
                FirstName = c.FirstName,
                Password = c.Password,
                StartTime = c.StartTime,
                SurName = c.SurName,
                UserName = c.UserName,
                UserStatus = c.UserStatus,
                UserTeamId = c.UserTeamId,
                UserToken = c.UserToken,
                UserType = c.UserType,
                UserRole = UserRole[c.UserType],
                WorkingDays = c.WorkingDays
            }).ToList();

            return View("User", UserList.UserListViewModel);
        }

        public ActionResult UserDetails(int id)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            UserViewModelList UserDetails;
            if (id != 0)
            {
                var c = _IUserService.GetUserDetails(id);
                UserDetails = new UserViewModelList
                {
                    Id = c.Id,
                    Cellphone = c.Cellphone,
                    CompanyId = c.CompanyId,
                    EmergencyContact = c.EmergencyContact,
                    EmergencyContactNo=c.EmergencyContactNo,
                    Email = c.Email,
                    EndTime = c.EndTime==null?DateTime.Now:c.EndTime,
                    FirstName = c.FirstName,
                    Password = c.Password,
                    StartTime = c.StartTime==null?DateTime.Now:c.StartTime,
                    SurName = c.SurName,
                    UserName = c.UserName,
                    UserStatus = c.UserStatus,
                    UserTeamId = c.UserTeamId,
                    UserToken = c.UserToken,
                    UserType = c.UserType,
                    WorkingDays = c.WorkingDays
                };
                //UserList.UserDetails = UserDetails;
                //return View("UserDetails", UserDetails);
            }
            else
            {
                UserDetails = new UserViewModelList
                {
                    Id = 0,
                    Cellphone = null,
                    CompanyId = CompanyId,
                    EmergencyContact = null,
                    EmergencyContactNo= null,
                    Email = null,
                    EndTime =DateTime.Now ,
                    FirstName = null,
                    Password = null,
                    StartTime = DateTime.Now,
                    SurName = null,
                    UserName = null,
                    UserStatus = null,
                    UserTeamId = 0,
                    UserType = 0,
                    WorkingDays = null
                };
            }
            UserList.UserDetails = UserDetails;
            var y=_commonservice.GetCompanySelectList(CompanyId);
            UserList.CompanyList = y.CompanyList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            var grp = _commonservice.GetGroupSelectList(CompanyId);
            UserList.GroupList = grp.GroupList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            return View("UserDetails", UserList);
        }

        [HttpPost]
        public JsonResult SaveDetails(UserViewModelList user)
        {
            UserServiceModel UserModel;
            UserModel = new UserServiceModel()
            {
                Id = user.Id,
                Cellphone = user.Cellphone,
                CompanyId = user.CompanyId,
                EmergencyContact = user.EmergencyContact,
                EmergencyContactNo=user.EmergencyContactNo,
                Email = user.Email,
                EndTime = user.EndTime,
                FirstName = user.FirstName,
                Password = user.Password,
                StartTime = user.StartTime,
                SurName = user.SurName,
                UserName = user.UserName,
                UserStatus = user.UserStatus,
                UserTeamId = user.UserTeamId,
                UserToken = user.UserToken,
                UserType = user.UserType,
                WorkingDays = user.WorkingDays
            };
            _IUserService.SaveUserDetails(UserModel);
            bool success = true;
            return Json(success, JsonRequestBehavior.AllowGet);
        }
    }
}