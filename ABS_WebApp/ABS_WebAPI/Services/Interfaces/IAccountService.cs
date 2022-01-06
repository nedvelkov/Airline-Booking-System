﻿using System;

namespace ABS_WebAPI.Services.Interfaces
{
    public interface IAccountService
    {
        public string CreateUser(string firstName, string lastName, string email, string password, int role);

        public string LoginUser(string email, string password);
    }
}