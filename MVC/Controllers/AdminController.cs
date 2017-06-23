using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC.custom_classes;
using MVC.Models.AdminViewModels;

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

        // GET: Admin/AddMovie
        [AuthenticatedUser("admin")]
        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        [AuthenticatedUser("admin")]
        public ActionResult AddMovie(AddMovieViewModel viewModel)
        {
            return View(viewModel);
        }


    }
}