using QconzLocate.Models;
using QconzLocateService.QconzLocateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QconzLocate.Controllers
{
    public class UserController : Controller
    {
        private UserService _IUserService = new UserService();
        private List<UserViewModel> UserList = new List<UserViewModel>();
       
        public UserController()
        {

        }
        // GET: User
        public ActionResult UserReport()
        {
            UserListViewModel users1 = new UserListViewModel();
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
                WorkingDays = c.WorkingDays
            }).ToList();

            users1.users = UserList;
            return View("User", users1);
        }

        //public ActionResult UserDetails(int id)
        //{
        //    UserViewModel UserDetails;
        //    var c = _IUserService.GetUserDetails(id);
        //    UserDetails = new UserViewModel
        //    {
        //        Id = c.Id,
        //        Cellphone = c.Cellphone,
        //        CompanyId = c.CompanyId,
        //        EmergencyContact = c.EmergencyContact,
        //        Email = c.Email,
        //        EndTime = c.EndTime,
        //        FirstName = c.FirstName,
        //        Password = c.Password,
        //        StartTime = c.StartTime,
        //        SurName = c.SurName,
        //        UserName = c.UserName,
        //        UserStatus = c.UserStatus,
        //        UserTeamId = c.UserTeamId,
        //        UserToken = c.UserToken,
        //        UserType = c.UserType,
        //        WorkingDays = c.WorkingDays
        //    };
        //    return View("UserDetails", UserDetails);
        //}
    }
}