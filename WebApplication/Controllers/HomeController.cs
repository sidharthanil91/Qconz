using QconzLocate.Models;
using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System.Web.Mvc;

namespace QconzLocate.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class HomeController : Controller
    {
        
        private ILocationService _ILocationService = new LocationService();
        public ActionResult DashboardV1()
        {
            return View();
        }
        public ActionResult Locator()
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var y = _ILocationService.GetCustomerLocation(CompanyId,null,0).ToArray();//.ConvertAll<object>(item => (object)item).ToArray();
                                                                      //y.Select(x => new string[] { x.Property1, x.Property2, x.Property3 });
            string markers = "[";
          
                    foreach( var item in y)
                    {
                        markers += "{";
                        markers += string.Format("'Address': '{0}',", item.Address);
                        markers += string.Format("'Lat': '{0}',", item.Lat);
                        markers += string.Format("'Lng': '{0}',", item.Lng);
                       // markers += string.Format("'description': '{0}'", sdr["Description"]);
                        markers += "},";
                    }
               
            markers += "];";
            ViewBag.Markers = markers;
            string[] countries = { "abc", "def", "hij" };
            ViewBag.countries = countries;
            return View();
        }

        public JsonResult GetSearchItem()
        {
            string[] countries = { "abc","def","hij"};
            return Json(countries);
        }

      [HttpPost]
        public JsonResult Search(string Customer)
        {
            int CompanyId = (int)(Session["CompanyId"]);
            var y = _ILocationService.GetCustomerLocation(CompanyId, Customer, 0).ToArray();
            //string markers = "[";

            //foreach (var item in y)
            //{
            //    markers += "{";
            //    markers += string.Format("'title': '{0}',", item.Address);
            //    markers += string.Format("'lat': '{0}',", item.Lat);
            //    markers += string.Format("'lng': '{0}',", item.Lng);
            //    // markers += string.Format("'description': '{0}'", sdr["Description"]);
            //    markers += "},";
            //}

            //markers += "];";
            //ResponseViewModel Messeage = new ResponseViewModel()
            //{
            //    Success = true,
            //    Message = y
            //};
            return Json(y, JsonRequestBehavior.AllowGet);
        
         }
    }
}