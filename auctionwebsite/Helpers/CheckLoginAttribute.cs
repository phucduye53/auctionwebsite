using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auctionwebsite.Helpers
{
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        public int Permission { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CurrentContext.IsLogged() == false)
            {
                filterContext.Result =
                    new RedirectResult("~/User/Login");
                return;
            }
            if (CurrentContext.GetCurUser().UserLevel < this.Permission)
            {
                filterContext.Result =
                    new HttpUnauthorizedResult();
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}