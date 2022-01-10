using System;
using System.Threading.Tasks;
using ABS_WebApp.Users;
using ABS_WebApp.ViewModels;

namespace ABS_WebApp.Services.Interfaces
{
    public interface IAccountService
    {
        public Tuple<bool,string> CreateUser(RegisterViewModel model);

        public bool LoginUser(LoginViewModel model);

        public UserClaims GetClaims(string email);

        public bool FindUser(string email);

        public bool ResetPassword(ResetPasswordViewModel model);

        public void SeedAdmin();

    }
}
