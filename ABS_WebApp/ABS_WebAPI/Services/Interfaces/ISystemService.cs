using System.Threading.Tasks;
using ABS_Models;

namespace ABS_WebAPI.Services.Interfaces
{
    public interface ISystemService
    {
        public Task<SystemDetailsModel> Details();

        public Task<bool> SeedData();
    }
}
