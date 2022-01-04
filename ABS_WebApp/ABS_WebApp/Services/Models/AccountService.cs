using System;
using System.Threading.Tasks;

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
           return await _webApiService.CreateUser(model);
        }

        public Task<bool> FindUser(string email) => throw new NotImplementedException();
        public Task<string> LogInUser(LoginViewModel model) => throw new NotImplementedException();
        public Task<bool> ResetPassword(RegisterViewModel model) => throw new NotImplementedException();
    }
}
