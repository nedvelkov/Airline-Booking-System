using ABS_SystemManager.Interfaces;

using ABS_WebAPI.Services.Interfaces;

namespace ABS_WebAPI.Services.Models
{
    public class AccountService : IAccountService
    {
        private readonly ISystemManager _systemManger;

        public AccountService(ISystemManager systemManger) => _systemManger = systemManger;
        public string CreateUser(string firstName, string lastName, string email, string password, int role)
            => _systemManger.CreateUser(firstName, lastName, email, password, role);
        public string LoginUser(string email, string password) => _systemManger.LoginUser(email, password);
    }
}
