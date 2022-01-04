using Microsoft.AspNetCore.Mvc;

using ABS_Models;
using ABS_WebAPI.Services.Interfaces;

using static ABS_DataConstants.DataConstrain;


namespace ABS_WebAPI.Controllers
{
    [Route(ACCOUNT_API_PATH)]
    [ApiController]
    public class AccountsController: ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService) => _accountService = accountService;

        [HttpPost]
        public ActionResult<string> CreateUser(RegisterModel model)
        {
            var result = _accountService.CreateUser(model.FirstName,model.LastName,model.Email,model.Password,model.Role);
            if (result.Contains(SUCCESSFULL_OPERATION))
            {
                return result;
            }
            return UnprocessableEntity(result);
        }
        
    }
}
