using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockParadise.Models
{
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Username"] == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
           
        }
    }

}