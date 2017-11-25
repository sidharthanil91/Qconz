using System.Web.Mvc;

namespace QconzLocate.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult DashboardV1()
        {
            return View();
        }
        public ActionResult DashboardV2()
        {
            return View();
        }
    }
}