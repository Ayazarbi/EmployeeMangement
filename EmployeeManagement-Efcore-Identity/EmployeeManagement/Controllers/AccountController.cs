using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{

    public class AccountController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmployeeRepository employeeRepository)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this.employeeRepository = employeeRepository;
        }


        //*****Registrattion Code**********////

        //[HttpGet]
        //public IActionResult Signup()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Signup(Signupmodel user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var newuser = new IdentityUser { UserName = user.Email, Email = user.Email };
        //        var result = await UserManager.CreateAsync(newuser, user.Password);

        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(newuser, isPersistent: false);
        //            return RedirectToAction("index", "employee");
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }

        //    }
        //    return View(user);




        //}

        
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Loginmodel user, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await UserManager.FindByEmailAsync(user.Email);

                if (loggedInUser != null)
                {
                    var result = await SignInManager.PasswordSignInAsync(loggedInUser.UserName, user.Password, user.Rememberme, false); ;

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("index", "employee");
                        }



                    }
                }

                ModelState.AddModelError("", "Invalid Credentials");


            }
            return View(user);




        }

        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetViewModel rm)
        {

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(rm.Email);

                if (user != null)
                {
                    Signupmodel resetmodal = new Signupmodel();
                    resetmodal.Email = user.Email;

                    return View("~/Views/Account/ResetForm.cshtml", resetmodal);
                }
                ModelState.AddModelError("", "user does not exist");
            }


            return View();

        }

        [HttpGet]
        public ActionResult ResetForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ResetForm(Signupmodel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                if (user != null)
                {

                    var result = await UserManager.ResetPasswordAsync(user, token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "something went wrong Please try Again");
                }
            }
            return View();

        }

        public ActionResult AccessDenied()
        {
            return View();
        }




    }
}
        