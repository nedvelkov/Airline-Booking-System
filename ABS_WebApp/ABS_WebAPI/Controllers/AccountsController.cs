using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

using ABS_Models;
using ABS_WebAPI.Services.Interfaces;

using static ABS_DataConstants.DataConstrain;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ABS_WebAPI.Controllers
{
    [Route(ACCOUNT_API_PATH)]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService) => _accountService = accountService;

        [HttpPost(USER_REGISTER)]
        public ActionResult<string> RegisterUser(RegisterModel model)
        {
            var result = _accountService.CreateUser(model.FirstName, model.LastName, model.Email, model.Password, model.Role);
            if (result.Contains(SUCCESSFULL_OPERATION))
            {
                return result;
            }
            return UnprocessableEntity(result);
        }

        [HttpPost(USER_LOGIN)]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = _accountService.LoginUser(model.Email, model.Password);
            if (result.Contains(SUCCESSFULL_OPERATION))
            {
                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,model.Email),
                    new Claim(ClaimTypes.Expiration,System.DateTimeOffset.Now.AddMinutes(5).ToString())

                }, COOKIE_SHEME_NAME);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = System.DateTimeOffset.Now.AddSeconds(30),
                    IsPersistent = false
                };

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await Response.HttpContext.SignInAsync(COOKIE_SHEME_NAME, claimsPrincipal, authProperties);

                HttpContext.Response.Cookies.Append(COOKIE_SHEME_NAME, claimsPrincipal.ToString(),
                    new Microsoft.AspNetCore.Http.CookieOptions { Expires = authProperties.ExpiresUtc });

                return NoContent();
            }
            return UnprocessableEntity(result);
        }

    }
}
