using System.Threading.Tasks;

namespace ABS_WebAPI.Services.Interfaces
{
    public interface ISystemService
    {
        public Task<string> Details();

        public Task<bool> SeedData();
    }
}
