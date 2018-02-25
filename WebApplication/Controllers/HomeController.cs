using QconzLocate.Models;
using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System;
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
            var y = _ILocationService.GetUserLocation(CompanyId, 0, 0).ToArray();
            string markers = "[";

            foreach (var item in y)
            {
                markers += "{";
                markers += string.Format("'Address': '{0}',", item.Address);
                markers += string.Format("'Lat': '{0}',", item.Lat);
                markers += string.Format("'Lng': '{0}',", item.Lng);
                markers += "},";
            }

            markers += "];";
            ViewBag.UserMarkers = markers;
            var Group = _commonservice.GetGroupSelectList(CompanyId);
            var User = _commonservice.GetUserSelectList(CompanyId);
            var DefaultItem = new SelectListItems()
            {
                id=0,
                text="All"
            };
           
            var items = new HomeViewModel();
            items.GroupLists = Group.GroupList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            items.UserLists = User.UserList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            items.UserLists.Insert(0, DefaultItem);
            items.GroupLists.Insert(0, DefaultItem);
            return View(items);
        }
        public ActionResult Locator()
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var y = _ILocationService.GetCustomerLocation(CompanyId,null,0,0).ToArray();

            string markers = "[";

            foreach (var item in y)
            {
                markers += "{";
                markers += string.Format("'Address': '{0}',", item.Address);
                markers += string.Format("'Lat': '{0}',", item.Lat);
                markers += string.Format("'Lng': '{0}',", item.Lng);
                markers += string.Format("'Type': '{0}',", item.Type);
                markers += "},";
            }

            markers += "];";
            ViewBag.Markers = markers;
            var Group = _commonservice.GetGroupSelectList(CompanyId);
            var User = _commonservice.GetUserSelectList(CompanyId);
            var DefaultItem = new SelectListItems()
            {
                id = 0,
                text = "All"
            };

            var items = new HomeViewModel();
            items.GroupLists = Group.GroupList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            items.UserLists = User.UserList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            items.UserLists.Insert(0, DefaultItem);
            items.GroupLists.Insert(0, DefaultItem);
            return View(items);
        }
        public ActionResult History()
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var User = _commonservice.GetUserSelectList(CompanyId);
            var items = new HomeViewModel();
            items.UserLists = User.UserList.Select(t => new SelectListItems
            {
                id = t.Id,
                text = t.Text
            }).ToList();
            var history = _ILocationService.GetHistoryLocation(CompanyId, User.UserList.FirstOrDefault().Id, null);
            items.HistoryGrid = history.Select(t => new HistoryGridModel { User = t.Address,Latitude=t.Lat,Longitude=t.Lng }).ToList();
             var y=history.ToArray();

            string markers = "[";

            foreach (var item in y)
            {
                markers += "{";
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
      
      
      [HttpPost]
        public JsonResult Search(string Customer,int UserId,int GroupId)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var y = _ILocationService.GetCustomerLocation(CompanyId, Customer, UserId, GroupId).ToArray();
           
            return Json(y, JsonRequestBehavior.AllowGet);
        
         }

        [HttpPost]
        public JsonResult Filter(int UserId,int GroupId)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var y = _ILocationService.GetUserLocation(CompanyId, UserId, GroupId).ToArray();
            return Json(y, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult HistoryFilter(int UserId,DateTime? Date)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var y = _ILocationService.GetHistoryLocation(CompanyId,UserId,Date).ToArray();
            return Json(y, JsonRequestBehavior.AllowGet);

        }
    }
}