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
    [SessionExpireFilter]
    [Authorize(Roles = "SUPER,ADMIN")]
    public class UserController : Controller
    {
        private IUserService _IUserService = new UserService();
        private UserViewModel UserList = new UserViewModel();
        private CommonService _commonservice = new CommonService();

        public UserController()
        {

        }
        // GET: All Users
        public ActionResult UserReport()
        {
            try
            {
                var user = System.Web.HttpContext.Current.User;
                int CompanyId = (int)(Session["CompanyId"]);
                if (CompanyId == 0)
                {
                    var CompanyLists = _commonservice.GetCompanySelectList(0);
                    var DefaultItem = new SelectListItems()
                    {
                        id = 0,
                        text = "All"
                    };
                    UserList.CompanyList = CompanyLists.CompanyList.Select(t => new SelectListItems
                    {
                        id = t.Id,
                        text = t.Text
                    }).ToList();
                    UserList.CompanyList.Insert(0, DefaultItem);
                }
                UserListViewModel users1 = new UserListViewModel();
                Dictionary<int, string> UserRole
                           = new Dictionary<int, string>() {
                         {2,"Company Admin"},
                         {3,"Dashboard and App"},
                         {4,"App Only" },
                           {5,"Web Only" }};

                UserList.UserListViewModel = _IUserService.GetAllUsers(CompanyId,"A").Select(c => new UserViewModelList
                {
                    Id = c.Id,
                    Cellphone = c.Cellphone,
                    CompanyId = c.CompanyId,
                    Company = c.Company,
                    EmergencyContact = c.EmergencyContact,
                    EmergencyContactNo = c.EmergencyContactNo,
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
                    WorkingDays = c.WorkingDays,
                    BaseLatitude=c.BaseLatitude,
                    BaseLongitude=c.BaseLongitude,
                    DefaultGroup=c.DefaultGroup,
                    IsContractor=c.IsContractor
                }).ToList();

                return View("User", UserList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Get Individual user
        public ActionResult UserDetails(int id)
        {
            try
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
                        EmergencyContactNo = c.EmergencyContactNo,
                        Email = c.Email,
                        EndTime = c.EndTime == null ? DateTime.Now : c.EndTime,
                        FirstName = c.FirstName,
                        Password = c.Password,
                        StartTime = c.StartTime == null ? DateTime.Now : c.StartTime,
                        SurName = c.SurName,
                        UserName = c.UserName,
                        UserGroups = c.UserGroups,
                        UserStatus = c.UserStatus,
                        UserTeamId = c.UserTeamId,
                        UserToken = c.UserToken,
                        UserType = c.UserType,
                        WorkingDays = c.WorkingDays,
                        BaseLatitude = c.BaseLatitude,
                        BaseLongitude = c.BaseLongitude,
                        DefaultGroup = c.DefaultGroup,
                        IsContractor = c.IsContractor
                    };
                }
                else
                {
                    UserDetails = new UserViewModelList
                    {
                        Id = 0,
                        Cellphone = null,
                        CompanyId = CompanyId,
                        EmergencyContact = null,
                        EmergencyContactNo = null,
                        Email = null,
                        EndTime = DateTime.ParseExact("2009-05-08 17:00:00,531", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture),
                        FirstName = null,
                        Password = null,
                        StartTime = DateTime.ParseExact("2009-05-08 08:00:00,531", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture),
                        SurName = null,
                        UserName = null,
                        UserStatus = "A",
                        UserTeamId = 0,
                        UserType = 0,
                        WorkingDays = "1,2,3,4,5",
                        BaseLatitude = null,
                        BaseLongitude = null,
                        DefaultGroup = null,
                        IsContractor = "N"
                    };
                }
                UserList.UserDetails = UserDetails;
                var y = _commonservice.GetCompanySelectList(CompanyId);
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
            catch (Exception ex)
            {
                return null;
            }
        }

        //Save User details
        [HttpPost]
        public JsonResult SaveDetails(UserViewModelList user)
        {
            try
            {
                UserServiceModel UserModel;
                UserModel = new UserServiceModel()
                {
                    Id = user.Id,
                    Cellphone = user.Cellphone,
                    CompanyId = user.CompanyId,
                    EmergencyContact = user.EmergencyContact,
                    EmergencyContactNo = user.EmergencyContactNo,
                    Email = user.Email,
                    EndTime = user.EndTime,
                    FirstName = user.FirstName,
                    Password = user.Password,
                    StartTime = user.StartTime,
                    SurName = user.SurName,
                    UserName = user.UserName,
                    UserGroups = user.UserGroups,
                    UserStatus = user.UserStatus,
                    UserTeamId = user.UserTeamId,
                    UserToken = user.UserToken,
                    UserType = user.UserType,
                    WorkingDays = user.WorkingDays,
                    BaseLatitude = user.BaseLatitude,
                    BaseLongitude = user.BaseLongitude,
                    DefaultGroup = user.DefaultGroup,
                    IsContractor = user.IsContractor
                };
                var message = _IUserService.SaveUserDetails(UserModel);
                bool Status = true;
                if (message != null)
                {
                    Status = false;
                }
                ResponseViewModel Messeage = new ResponseViewModel()
                {
                    Success = Status,
                    Message = message
                };

                return Json(Messeage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public JsonResult Filter(int CompanyId,string Status)
        {

            Dictionary<int, string> UserRole
                           = new Dictionary<int, string>() {
                         {2,"Company Admin"},
                         {3,"Dashboard and App"},
                         {4,"App Only" } ,{5,"Web Only" }};

            UserList.UserListViewModel = _IUserService.GetAllUsers(CompanyId, Status).Select(c => new UserViewModelList
            {
                Id = c.Id,
                Cellphone = c.Cellphone,
                CompanyId = c.CompanyId,
                Company = c.Company,
                EmergencyContact = c.EmergencyContact,
                EmergencyContactNo = c.EmergencyContactNo,
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
            return Json(UserList.UserListViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}