using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Routing;

namespace MVC.custom_classes
{
    public class AnonymousUserAttribute : AuthorizeAttribute
    {
        private string[] allowedRoles { get; set; }

        public AnonymousUserAttribute() : base()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            //Check if the 'Authenticated' session boolean is true:
            if (!(bool)httpContext.Session["Authenticated"])
            {
                authorize = true;
            }
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //The user is logged in, so direct them to the dashboard:
            RouteValueDictionary route = new RouteValueDictionary(){
                { "action", "Index" },
                { "controller", "Dashboard" }
            };
            filterContext.Result = new RedirectToRouteResult(route);
                                   
        }
    }
}