using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC.custom_classes;
using SharedResources.Interfaces;
using MVC.Models;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ////Get the session information:
            //IUserMapper user = SessionHelper.getSession();
            ////Create a DashboardViewModel:
            //DashboardViewModel dashboard = new DashboardViewModel();
            ////Set the user info into the viewmodel:
            //dashboard.Id = user.Id;
            //dashboard.Name = user.Name;
            //dashboard.RoleId = user.RoleId;
            //dashboard.RoleName = user.RoleName;
            //dashboard.user = user;
            ////Create a HomeViewModel:
            //HomeViewModel viewModel = new HomeViewModel();
            //viewModel.dashboardModel = dashboard;
            ////Return to view with the viewmodel.
            //return View(viewModel);

            return View();
        }
    }
}