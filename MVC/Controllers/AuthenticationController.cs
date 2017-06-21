using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC.Models;
using System.Web.SessionState;
using SharedResources.Interfaces;
using SharedResources.Mappers;
using CompositionRoot;

namespace MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        //Properties:
        private IUsersBLL users_bll { get; set; }

        //Constructor:
        public AuthenticationController()
        {
            CRoot CompositionRoot = new CRoot("test");
            users_bll = CompositionRoot.UsersBLL;
            //IMoviesBLL movies_bll = CompositionRoot.MoviesBLL;
        }

        // GET: Authentication
        public ActionResult Index()
        {
            //Redirect to login controller:
            return RedirectToAction("Login");
        }

        // GET: Authentication/Login
        public ActionResult Login()
        {
            LoginViewModel viewModel = new LoginViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            //Declare redirect path variables:
            string redirect_action;
            string redirect_controller;

            //Do authentication here:
            IUserMapper user = new UserMapper();
            user.Name = viewModel.Name;
            user.password_hash = viewModel.Password;
            bool isAuthentic = users_bll.authenticate_user(user);

            //If the user is authentic:
            if(isAuthentic){
                //Get the user's information:
                user = users_bll.Get_User_by_User_Name(user);

                //Populate Session data with user data from the database:
                Session["Authenticated"] = true;
                Session["Id"] = user.Id;
                Session["Name"] = user.Name;
                Session["RoleName"] = user.RoleName;
                Session["RoleId"] = user.RoleId;

                //Authentication succeeded.  Redirect to protected page:
                redirect_action = "Index";
                redirect_controller = "Admin";
            }
            else
            {
                //Authentication failed.  Redirect to login page:
                redirect_action = "Login";
                redirect_controller = "Authentication";      
            }
            //Redirect:
            return RedirectToAction(redirect_action, redirect_controller);
        }

    }
}