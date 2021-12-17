using System;
using System.Threading.Tasks;

using ABS_WebApp.Services.Interfaces;

namespace ABS_WebApp.Services.Models
{
    public class SystemService : ISystemService
    {
        private readonly WebApiService _webApiService;

        public SystemService(WebApiService webApiService) => _webApiService = webApiService;

        public async Task<string> Details()
        {
            try
            {
               return await _webApiService.GetSystemDetails();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
