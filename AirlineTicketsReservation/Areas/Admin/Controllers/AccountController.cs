﻿using AirlineTicketsReservation.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTicketsReservation.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email
                };

                var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    //asign this user the "User" role
                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                    if (roleIdentityResult.Succeeded)
                    {
                        //Show success notification
                        return RedirectToAction("Login");
                    }
                }
            }


            // Show error notification
            return View("Register");

        }


        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Email,
                 loginViewModel.Password, false, false);
            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return RedirectToPage(loginViewModel.ReturnUrl);
                }


                return RedirectToAction("Index", "Home");
            }
            // Show errors
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}

