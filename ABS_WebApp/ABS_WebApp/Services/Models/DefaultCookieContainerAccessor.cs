using System;
using System.Net;

using ABS_WebApp.Services.Interfaces;

namespace ABS_WebApp.Services.Models
{
    public class DefaultCookieContainerAccessor : ICookieContainerAccessor
    {
        private static Lazy<CookieContainer> _container = new Lazy<CookieContainer>();
        public CookieContainer CookieContainer => _container.Value;
    }
}
