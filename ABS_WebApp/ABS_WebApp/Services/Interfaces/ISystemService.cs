using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABS_WebApp.Services.Interfaces
{
    public interface ISystemService
    {
        public Task<string> Details();

        public void SeedData();
    }
}
