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
    public class UserController : Controller
    {
        private IUserService _IUserService = new UserService();
        private List<UserViewModel> UserList = new List<UserViewModel>();
       
        public UserController()
        {

        }
        // GET: User
        public ActionResult UserReport()
        {
            var user = System.Web.HttpContext.Current.User;
            UserListViewModel users1 = new UserListViewModel();
            Dictionary<int, string> UserRole
                       = new Dictionary<int, string>() {
                         {2,"Admin User"},
                         {3,"Dashboard User"},
                         {4,"App User" } };

            UserList = _IUserService.GetAllUsers().Select(c => new UserViewModel
            {
                Id = c.Id,
                Cellphone = c.Cellphone,
                CompanyId = c.CompanyId,
                EmergencyContact = c.EmergencyContact,
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

            users1.users = UserList;
            return View("User", users1);
        }

        public ActionResult UserDetails(int id)
        {
            UserViewModel UserDetails;
            if (id != 0)
            {
                var c = _IUserService.GetUserDetails(id);
                UserDetails = new UserViewModel
                {
                    Id = c.Id,
                    Cellphone = c.Cellphone,
                    CompanyId = c.CompanyId,
                    EmergencyContact = c.EmergencyContact,
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
                    WorkingDays = c.WorkingDays
                };
                return View("UserDetails", UserDetails);
            }
            else
            {
                UserDetails = new UserViewModel
                {
                    Id = 0,
                    Cellphone = null,
                    CompanyId = null,
                    EmergencyContact = null,
                    Email = null,
                    EndTime = null,
                    FirstName = null,
                    Password =null,
                    StartTime = null,
                    SurName = null,
                    UserName = null,
                    UserStatus = null,
                    UserTeamId = null,
                    UserToken = null,
                    UserType = 0,
                    WorkingDays = null
                };
                return View("UserDetails", UserDetails);
            }
        }

        [HttpPost]
        public JsonResult SaveDetails(UserViewModel user)
        {
            UserServiceModel UserModel;
            UserModel = new UserServiceModel()
            {
                Id = user.Id,
                Cellphone = user.Cellphone,
                CompanyId = user.CompanyId,
                EmergencyContact = user.EmergencyContact,
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