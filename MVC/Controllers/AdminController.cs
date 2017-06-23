using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC.custom_classes;

namespace MVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [AuthenticatedUser("admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}