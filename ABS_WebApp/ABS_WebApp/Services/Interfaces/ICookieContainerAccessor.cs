using System.Net;

namespace ABS_WebApp.Services.Interfaces
{
    public interface ICookieContainerAccessor
    {
        CookieContainer CookieContainer { get; }
    }
}
