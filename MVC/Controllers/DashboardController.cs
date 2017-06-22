using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC.custom_classes;
using MVC.Models;
using SharedResources.Interfaces;

namespace MVC.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [AuthenticatedUser("admin", "user")]
        public ActionResult Index()
        {
            //Get the session information:
            IUserMapper user = SessionHelper.getSession();
            //Create a DashboardViewModel:
            DashboardViewModel viewModel = new DashboardViewModel();
            //Set the user info into the viewmodel:
            viewModel.Id = user.Id;
            viewModel.Name = user.Name;
            viewModel.RoleId = user.RoleId;
            viewModel.RoleName = user.RoleName;
            viewModel.user = user;
            //Return to view with the viewmodel.
            return View(viewModel);
        }
    }
}