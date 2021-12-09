using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABS_WebApp.Services.Interfaces
{
    public interface IAirlineService
    {
        public Task<string> CreateAirline(string name);

        public Task<IReadOnlyList<string>> Airlines { get; }
    }
}
