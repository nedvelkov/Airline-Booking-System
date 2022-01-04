using ABS_WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABS_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DisplaySystemDetails", "App");
            }
            return View();
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DisplaySystemDetails", "App");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: register user
                return RedirectToAction("DisplaySystemDetails", "App");
            }
            return View(model);
        }

        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DisplaySystemDetails", "App");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: check is email valid
                TempData["email"] = model.Email;
                return RedirectToAction(nameof(ResetPassword));
            }
            return View(model);
        }

        public IActionResult ResetPassword()
        {
            var model = new ResetPasswordViewModel();
            model.Email = TempData["email"].ToString();
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: reset password
                return RedirectToAction(nameof(Login));
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                //TOD: sign out user
            }
            return RedirectToAction(nameof(Register));
        }
    }
}
