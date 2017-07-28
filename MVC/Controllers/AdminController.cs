using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC.custom_classes;
using MVC.Models.AdminViewModels;
using SharedResources.Interfaces;
using SharedResources.Mappers;
using CompositionRoot;
using SharedResources.Exceptions.BLL;
using MVC.Models;

namespace MVC.Controllers
{
    public class AdminController : Controller
    {
        IMoviesBLL movies_bll { get; set; }

        public AdminController()
        {
            CRoot CompositionRoot = new CRoot("prod");
            movies_bll = CompositionRoot.MoviesBLL; 
        }


        // GET: Admin
        [AuthenticatedUser("admin")]
        public ActionResult Index()
        {
            AddMovieViewModel viewModel = new AddMovieViewModel();

            if (TempData["ErrorMessage"] != null)
            {
                viewModel.errorMessage = TempData["ErrorMessage"].ToString();
            }

            return View(viewModel);
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
            //Declare redirect path variables:
            string redirect_action;
            string redirect_controller;

            try
            {
                movies_bll.InsertMovie(new MovieMapper { Title = viewModel.Title, RunTime = viewModel.RunTime, Image = viewModel.Image });
                //redirect path variables:
                redirect_action = "index";
                redirect_controller = "Admin";
            }
            catch (BLLException)
            {
                //redirect path variables:
                redirect_action = "index";
                redirect_controller = "Admin";
                TempData["ErrorMessage"] = "Something went wrong.  Please try again.";
            }
            //Redirect to the dashboard (protected page):
            return RedirectToAction(redirect_action, redirect_controller);
            //return View(viewModel);
        }


    }
}