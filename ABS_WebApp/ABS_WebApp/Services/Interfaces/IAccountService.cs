using System;
using System.Threading.Tasks;
using ABS_Models;
using ABS_WebApp.ViewModels;

namespace ABS_WebApp.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<Tuple<bool,string>> CreateUser(RegisterViewModel model);

        public Task<string> LoginUser(LoginModel model);

        public Task<bool> FindUser(string email);

        public Task<bool> ResetPassword(RegisterViewModel model);

    }
}
