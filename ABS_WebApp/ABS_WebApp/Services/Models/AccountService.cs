using System;
using System.Threading.Tasks;
using ABS_Models;
using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.ViewModels;

namespace ABS_WebApp.Services.Models
{
    public class AccountService : IAccountService
    {
        private readonly WebApiService _webApiService;

        public AccountService(WebApiService webApiService) => _webApiService = webApiService;

        public async Task<Tuple<bool,string>> CreateUser(RegisterViewModel model)
        {
           return await _webApiService.RegisterUser(model);
        }

        public Task<bool> FindUser(string email) => throw new NotImplementedException();
        public async Task<string> LoginUser(LoginModel model) => await _webApiService.LoginUser(model);
        public Task<bool> ResetPassword(RegisterViewModel model) => throw new NotImplementedException();
    }
}
