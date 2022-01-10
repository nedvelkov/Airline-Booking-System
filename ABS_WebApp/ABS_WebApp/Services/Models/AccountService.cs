using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.Users;
using ABS_WebApp.ViewModels;

using static ABS_WebApp.Users.UserConstants;

namespace ABS_WebApp.Services.Models
{
    public class AccountService : IAccountService
    {
        private Dictionary<string, UserModel> _users;
        private readonly IConfiguration _configuration;

        public AccountService(IConfiguration configuration)
        {
            _users = new Dictionary<string, UserModel>();
            _configuration = configuration;
        }

        public Tuple<bool, string> CreateUser(RegisterViewModel model)
        {
            if (_users.ContainsKey(model.Email))
            {
                return new Tuple<bool, string>(false, EXISTING_EMAIL);
            }
            var hashPasword = HashPassword(model.Password);
            model.Password = hashPasword;
            _users.Add(model.Email, model);
            return new Tuple<bool, string>(true, SUCCESSFUL_CREATED_USER);
        }

        public bool LoginUser(LoginViewModel model)
        {
            if (_users.ContainsKey(model.Email) == false)
            {
                return false;
            }
            var pas = HashPassword(model.Password);
            return CompareHash(model.Password, _users[model.Email].Password);
        }

        public UserClaims GetClaims(string email)
        {
            var user = _users[email];
            var fullName = user.FirstName + "_" + user.LastName;
            return new UserClaims()
            {
                FullName = fullName,
                Role = user.Role
            };
        }

        public bool FindUser(string email) => _users.ContainsKey(email);
        public bool ResetPassword(ResetPasswordViewModel model)
        {
            if (!_users.ContainsKey(model.Email) == false)
            {
                return false;
            }

            var hashedPassword = HashPassword(model.Password);
            _users[model.Email].Password = hashedPassword;
            return true;
        }

        private string HashPassword(string password)
        {
            byte[] unhashedBytes = Encoding.Unicode.GetBytes(String.Concat(_configuration["Encrypt:Salt"], password));

            SHA256Managed sha256 = new SHA256Managed();
            byte[] hashedBytes = sha256.ComputeHash(unhashedBytes);
            return Convert.ToBase64String(hashedBytes);
        }

        private bool CompareHash(string attemptedPassword, string hashPassword)
            => HashPassword(attemptedPassword) == hashPassword;

        public void SeedAdmin()
        {
            var adminEmail = _configuration["AdminPanel:AdminEmail"];
            if (!_users.ContainsKey(adminEmail))
            {
                var admin = new UserModel()
                {
                    Email = adminEmail,
                    FirstName = _configuration["AdminPanel:AdminFirstName"],
                    LastName = _configuration["AdminPanel:AdminLastName"],
                    Role = ADMIN_ROLE,
                    Password = _configuration["AdminPanel:AdminPassword"]
                };
                var hashAdminPassword = HashPassword(admin.Password);
                admin.Password = hashAdminPassword;
                _users.Add(adminEmail, admin);
            }
        }

    }
}
