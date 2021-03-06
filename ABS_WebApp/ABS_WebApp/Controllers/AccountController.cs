using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

using ABS_WebApp.ViewModels;
using ABS_WebApp.Services.Interfaces;

using static ABS_WebApp.Users.UserConstants;
using static ABS_DataConstants.DataConstrain;
using Microsoft.Extensions.Configuration;

namespace ABS_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {

                if (User.IsInRole(USER_ROLE))
                {
                    return RedirectToAction("DisplaySystemDetails", "App");
                }
                return RedirectToAction("DisplaySystemDetails", "Admin");

            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DisplaySystemDetails", "App");
            }
            if (_accountService.LoginUser(loginModel))
            {
                var claimsUser = _accountService.GetClaims(loginModel.Email);

                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email,loginModel.Email),
                    new Claim(ClaimTypes.Name,claimsUser.FullName),
                    new Claim(ClaimTypes.Role,claimsUser.Role)

                }, COOKIE_SHEME_NAME);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false
                };
                if (!loginModel.RememberMe)
                {
                    var minutes = int.Parse(_configuration["ExpireCookieTime:NotPersistentAsMinutes"].ToString());
                    authProperties.ExpiresUtc = System.DateTimeOffset.Now.AddMinutes(minutes);
                }
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                try
                {

                    await Response.HttpContext.SignInAsync(COOKIE_SHEME_NAME, claimsPrincipal, authProperties);

                    HttpContext.Response.Cookies.Append(COOKIE_SHEME_NAME, claimsPrincipal.ToString(),
                        new Microsoft.AspNetCore.Http.CookieOptions { Expires = authProperties.ExpiresUtc });
                }
                catch (System.Exception)
                {
                    return RedirectToAction("Error", "App");
                }

                if (claimsUser.Role == USER_ROLE)
                {
                    return RedirectToAction("DisplaySystemDetails", "App");
                }
                return RedirectToAction("DisplaySystemDetails", "Admin", new { area = "Admin" });
            }
            ModelState.AddModelError("", string.Format(MISSING_EMAIL, loginModel.Email));
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
            string error = "";
            if (ModelState.IsValid)
            {
                var result = _accountService.CreateUser(model);
                if (result.Item1)
                {
                    return RedirectToAction(nameof(Login));
                }
                error = result.Item2;
            }
            ModelState.AddModelError("", error);
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
                if (_accountService.FindUser(model.Email))
                {
                    TempData["email"] = model.Email;
                    return RedirectToAction(nameof(ResetPassword));
                }
                ModelState.AddModelError("", string.Format(MISSING_EMAIL, model.Email));
                return View(model);
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
                if (_accountService.ResetPassword(model))
                {
                    return RedirectToAction(nameof(Login));
                }
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(COOKIE_SHEME_NAME);
            }
            return RedirectToAction(nameof(Login), "Account");
        }
    }
}
