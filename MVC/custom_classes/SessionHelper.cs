using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.SessionState;
using SharedResources.Interfaces;
using SharedResources.Mappers;

namespace MVC.custom_classes
{
    public static class SessionHelper
    {
        public static IUserMapper getSession()
        {
            return HttpContext.Current.Session["UserMapper"] as IUserMapper;
        }
    }
}