using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockParadise.Models
{
    public class CheckUserRoleAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Role"] == null || (string)filterContext.HttpContext.Session["Role"] != "admin")
            {
                filterContext.Result = new RedirectResult("~/Home");
            }
        }
    }

}