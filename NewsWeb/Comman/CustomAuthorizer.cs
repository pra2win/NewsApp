using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewsWeb.Comman
{
    public class CustomAuthorizer : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isAuthorized = filterContext.HttpContext.Session["UserId"] != null; // check authorization
            base.OnAuthorization(filterContext);
            if (!isAuthorized && !filterContext.ActionDescriptor.ActionName.Equals("Login", StringComparison.InvariantCultureIgnoreCase)
                && !filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Equals("AccountController", StringComparison.InvariantCultureIgnoreCase))
            {

                HandleUnauthorizedRequest(filterContext);

            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result =
           new RedirectToRouteResult(
               new RouteValueDictionary{{ "controller", "Account" },
                                          { "action", "Login" }

                                         });

        }
    }

}