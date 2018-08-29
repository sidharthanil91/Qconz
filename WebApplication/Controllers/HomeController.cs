using QconzLocate.Models;
using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace QconzLocate.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class HomeController : Controller
    {
        private CommonService _commonservice = new CommonService();
        private ILocationService _ILocationService = new LocationService();
        public ActionResult DashboardV1()
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var y = _ILocationService.GetUserLocation(CompanyId, 0, 0, null, null,null).ToArray();
            string markers = "[";

            foreach (var item in y)
            {
                markers += "{";
                markers += string.Format("'UserId': '{0}',", item.UserId);
                markers += string.Format("'Name': '{0}',", item.Name);
                markers += string.Format("'Address': '{0}',", item.Address);
                markers += string.Format("'Lat': '{0}',", item.Lat);
                markers += string.Format("'Lng': '{0}',", item.Lng);
                markers += string.Format("'ShowPin': '{0}',", true);
                markers += "},";
            }

            markers += "];";
            ViewBag.UserMarkers = markers;
            var Group = _commonservice.GetGroupSelectList(CompanyId);
            var Customer = _commonservice.GetCustomerSelectList(CompanyId);
            var DefaultItem = new SelectListItems()
            {
                id=0,
                text="All"
            };
            var DefaultItem1 = new SelectListItems()
            {
                id = 0,
                text = ""
            };
            var items = new HomeViewModel();
            items.GroupLists = Group.GroupList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            items.CustomerLists = Customer.CustomerList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            //items.CustomerLists.Insert(0, DefaultItem1);
            items.GroupLists.Insert(0, DefaultItem);
            items.TeamDetails = _commonservice.GetMapUserSelectList(CompanyId,0).UserList.Select(t => new TeamDetailsModel { UserId = t.Id, Name = t.Text, BaseDetail = false, ShowOnMap = false, ShowPin = false }).ToList();
            return View(items);
        }
        //public ActionResult Locator()
        //{
        //    int CompanyId = (int)(Session["CompanyId"]);
        //    var y = _ILocationService.GetCustomerLocation(CompanyId,null,0,0).ToArray();

        //    string markers = "[";

        //    foreach (var item in y)
        //    {
        //        markers += "{";
        //        markers += string.Format("'UserId': '{0}',", item.UserId);
        //        markers += string.Format("'Address': '{0}',", item.Address);
        //        markers += string.Format("'Name': '{0}',", item.Name);
        //        markers += string.Format("'Lat': '{0}',", item.Lat);
        //        markers += string.Format("'Lng': '{0}',", item.Lng);
        //        markers += string.Format("'Type': '{0}',", item.Type);
        //        markers += "},";
        //    }

        //    markers += "];";
        //    ViewBag.Markers = markers;
        //    var Customer = _commonservice.GetCustomerSelectList(CompanyId);
        //    var Group = _commonservice.GetGroupSelectList(CompanyId);
        //    var User = _commonservice.GetUserSelectList(CompanyId);
        //    var DefaultItem = new SelectListItems()
        //    {
        //        id = 0,
        //        text = "All"
        //    };

        //    var items = new HomeViewModel();
        //    items.GroupLists = Group.GroupList.Select(t => new SelectListItems
        //    {
        //        id = t.Id,
        //        text = t.Text
        //    }).ToList();
        //    items.UserLists = User.UserList.Select(t => new SelectListItems
        //    {
        //        id = t.Id,
        //        text = t.Text
        //    }).ToList();
        //    items.CustomerLists = Customer.CustomerList.Select(t => new SelectListItems
        //    {
        //        id = t.Id,
        //        text = t.Text
        //    }).ToList();
        //    items.UserLists.Insert(0, DefaultItem);
        //    items.GroupLists.Insert(0, DefaultItem);
        //    return View(items);
        //}
        public ActionResult History(int Id=0,string Mode=null)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var User = _commonservice.GetUserSelectList(CompanyId);
            var items = new HomeViewModel();
            DateTime? StartDate = null;
            DateTime? EndDate = null;
            if (Mode == null)
            {
                var nzTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
                var utcNow = DateTime.UtcNow;
                var nzNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, nzTimeZoneInfo);
                var outputNz = nzNow.ToString("F", CultureInfo.GetCultureInfo("en-NZ"));
                //StartDate = DateTime.UtcNow.Date;
                //EndDate = DateTime.UtcNow.Date.AddDays(1);
                StartDate = Convert.ToDateTime(outputNz).Date;
                EndDate = Convert.ToDateTime(outputNz).Date;
                EndDate = EndDate.Value.AddDays(1);
            }
            items.UserLists = User.UserList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            int UserId = 0;//User.UserList.FirstOrDefault().Id;
            if (Id > 0)
            {
                UserId = Id;
            }
            var history = _ILocationService.GetHistoryLocation(CompanyId,UserId, StartDate,EndDate,Mode);
            items.HistoryGrid = history.Select(t => new HistoryGridModel { User = t.Name,Date=t.Address,Latitude=t.Lat,Longitude=t.Lng }).ToList();
           
            items.UserId = UserId;
            var y=history.ToArray();

            string markers = "[";

            foreach (var item in y)
            {
                markers += "{";
                markers += string.Format("'Name': '{0}',", item.Name);
                markers += string.Format("'Address': '{0}',", item.Address);
                markers += string.Format("'Lat': '{0}',", item.Lat);
                markers += string.Format("'Lng': '{0}',", item.Lng);
                markers += string.Format("'Type': '{0}',", item.Type);
                markers += "},";
            }

            markers += "];";
            ViewBag.Markers = markers;
            return View(items);
        }


        //[HttpPost]
        //  public JsonResult Search(string Customer,int UserId,int GroupId)
        //  {
        //      int CompanyId = (int)(Session["CompanyId"]);
        //      var y = _ILocationService.GetCustomerLocation(CompanyId, Customer, UserId, GroupId).ToArray();

        //      return Json(y, JsonRequestBehavior.AllowGet);

        //   }

        [HttpPost]
        public JsonResult GroupChange( int GroupId)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var y = _commonservice.GetMapUserSelectList(CompanyId, GroupId).UserList.Select(t => new TeamDetailsModel { UserId = t.Id, Name = t.Text, BaseDetail = false, ShowOnMap = false, ShowPin = false }).ToList();
            return Json(y, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Filter(int Customer, int GroupId,int[] ShowMap,int[] ShowPin,int []ShowBase)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var y = _ILocationService.GetCustomerLocation(CompanyId, Customer, GroupId,ShowMap,ShowPin, ShowBase).ToArray();
            return Json(y, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult HistoryFilter(int UserId,DateTime? StartDate, DateTime? EndDate)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            if (StartDate != null)
                StartDate = StartDate.Value.Date;
            if (EndDate != null)
            {
                EndDate = EndDate.Value.Date;
                EndDate = EndDate.Value.AddDays(1);
            }
            var y = _ILocationService.GetHistoryLocation(CompanyId,UserId, StartDate,EndDate,null).ToArray();
            return Json(y, JsonRequestBehavior.AllowGet);

        }
    }
}