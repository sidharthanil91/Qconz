using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Security;

namespace QconzLocate.Controllers
{
    public class SessionExpireFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

          
           
            if (ctx.Session["CompanyId"] == null)
            {
                // check if a new session id was generated
                FormsAuthentication.SignOut();
                filterContext.Result = new RedirectResult("~/Home/DashboardV1");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}