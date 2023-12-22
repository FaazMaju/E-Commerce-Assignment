using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Assignment.Services
{
    
    public class RequiresUserIdAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var userId = filterContext.HttpContext.Session["UserId"] as string;
            var userRole = filterContext.HttpContext.Session["UserRole"] as string;

            if (filterContext.ActionDescriptor.ActionName.Equals("Login", StringComparison.OrdinalIgnoreCase) &&
        filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Equals("Home", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            if(userRole=="2" && !filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Equals("Home"))
            {
                filterContext.Result = new RedirectResult("~/Home/Login?redirected='access denied'");
            }
            if(userRole=="2" && filterContext.ActionDescriptor.ActionName.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                filterContext.Result = new RedirectResult("~/Home/Login?redirected='access denied'");
            }

            if (string.IsNullOrEmpty(userId))
            {
                filterContext.Result = new RedirectResult("~/Home/Login?redirected='login required'");
            }

        }
    }

}