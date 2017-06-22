using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Routing;

namespace MVC.custom_classes
{
    public class AuthenticatedUserAttribute : AuthorizeAttribute
    {
        private string[] allowedRoles { get; set; }

        public AuthenticatedUserAttribute() : base()
        {
        }

        //Overloaded constructor to take arguments for allowed roles:
        public AuthenticatedUserAttribute(params string[] allowedRoles) : base()
        {
            this.allowedRoles = allowedRoles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            //Check if the 'Authenticated' session boolean is true:
            if ((bool)httpContext.Session["Authenticated"])
            {
                //Check if the session RoleName is in the list of allowed roles:
                foreach(string role in allowedRoles){
                    if(role == httpContext.Session["RoleName"].ToString()){
                        authorize = true;
                    }
                }
            }
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //Later this needs to redirect to a page based on user role:
            RouteValueDictionary route = new RouteValueDictionary(){
                { "action", "Index" },
                { "controller", "Home" }
            };
            filterContext.Result = new RedirectToRouteResult(route);
                                   
        }  
    }
}