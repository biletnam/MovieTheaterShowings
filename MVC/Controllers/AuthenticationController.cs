using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC.Models;
using System.Web.SessionState;
using SharedResources.Exceptions.BLL;
using SharedResources.Interfaces;
using SharedResources.Mappers;
using CompositionRoot;
using MVC.custom_classes;

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
        }

        // GET: Authentication
        public ActionResult Index()
        {
            //Redirect to login controller:
            return RedirectToAction("Login");
        }

        // GET: Authentication/Login
        [AnonymousUser]
        public ActionResult Login()
        {
            LoginViewModel viewModel = new LoginViewModel();

            if (TempData["ErrorMessage"] != null)
            {
                viewModel.errorMessage = TempData["ErrorMessage"].ToString();
            }

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

                setSessionData(user);

                //Authentication succeeded.  Redirect to protected page:
                redirect_action = "Index";
                redirect_controller = "Dashboard";
            }
            else
            {
                //Authentication failed.  Redirect to login page:
                redirect_action = "Login";
                redirect_controller = "Authentication";
                TempData["ErrorMessage"] = "The username or password you entered was incorrect.  Please try again.";
            }
            //Redirect:
            return RedirectToAction(redirect_action, redirect_controller);
        }

        //Method to handle populating session data:
        public void setSessionData(IUserMapper user)
        {
            //Populate Session data with user data from the database:
            Session["Authenticated"] = true;
            Session["Id"] = user.Id;
            Session["Name"] = user.Name;
            Session["RoleName"] = user.RoleName;
            Session["RoleId"] = user.RoleId;
            Session["UserMapper"] = user;
        }

        // GET: Authentication/Logout
        [AuthenticatedUser("admin", "user")]
        public ActionResult Logout()
        {
            //Clear the session:
            Session.Clear();
            Session["Authenticated"] = false;
            //Redirect to home:
            return RedirectToAction("Index", "Home");
        }

        // GET: Authentication/Signup
        [AnonymousUser]
        public ActionResult Signup()
        {
            SignupViewModel viewModel = new SignupViewModel();

            if (TempData["ErrorMessage"] != null)
            {
                viewModel.errorMessage = TempData["ErrorMessage"].ToString();
            }
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Signup(SignupViewModel viewModel)
        {
            //Declare redirect path variables:
            string redirect_action;
            string redirect_controller;

            //Verify the user's password and confirmPassword are the same, else GET OUT!
            if(viewModel.Password == viewModel.ConfirmPassword){
                //Create a new user:
                try
                {
                    IUserMapper user = users_bll.Insert(new UserMapper { Name = viewModel.Name, RoleName = "user", password_hash = viewModel.Password });

                    //Set the session to log the user in:
                    setSessionData(user);

                    //redirect path variables:
                    redirect_action = "Index";
                    redirect_controller = "Dashboard";
                }
                catch(SqlBLLException){
                    //redirect path variables:
                    redirect_action = "Signup";
                    redirect_controller = "Authentication";
                    TempData["ErrorMessage"] = "The username you entered already exists.";
                }
            }
            else
            {
                //redirect path variables:
                redirect_action = "Signup";
                redirect_controller = "Authentication";
                TempData["ErrorMessage"] = "The password you entered must match in both fields.";
            }

            //Redirect to the dashboard (protected page):
            return RedirectToAction(redirect_action, redirect_controller);
        }


    }
}