using QconzLocateService.QconzLocateInterface;
using QconzLocateService.QconzLocateService;
using System.Web.Mvc;

namespace QconzLocate.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private ILocationService _ILocationService = new LocationService();
        public ActionResult DashboardV1()
        {
            return View();
        }
        public ActionResult Locator()
        {
            var y = _ILocationService.GetCustomerLocation().ToArray();//.ConvertAll<object>(item => (object)item).ToArray();
                                                                      //y.Select(x => new string[] { x.Property1, x.Property2, x.Property3 });
            string markers = "[";
          
                    foreach( var item in y)
                    {
                        markers += "{";
                        markers += string.Format("'title': '{0}',", item.Address);
                        markers += string.Format("'lat': '{0}',", item.Lat);
                        markers += string.Format("'lng': '{0}',", item.Lng);
                       // markers += string.Format("'description': '{0}'", sdr["Description"]);
                        markers += "},";
                    }
               
            markers += "];";
            ViewBag.Markers = markers;
            return View();
        }
    }
}