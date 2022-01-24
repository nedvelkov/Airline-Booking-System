using System.Threading.Tasks;

namespace ABS_WebAPI.Services.Interfaces
{
    public interface ISystemService
    {
        public string Details();

        public Task<bool> SeedData();
    }
}
