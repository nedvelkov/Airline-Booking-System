using ABS_WebApp.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using ABS_WebApp.Services.RequestModels;

namespace ABS_WebApp.Services
{
    public class WebApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _webApiUrl;

        public WebApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _webApiUrl = configuration["WebApiUrl"];
            _httpClient.BaseAddress = new Uri(_webApiUrl);
        }

        #region Airport

        public async Task<IEnumerable<string>> GetAirports()
            => await _httpClient.GetFromJsonAsync<IEnumerable<string>>("/api/airport");

        public async Task<string> CreateAirport(AirportRequestModel airport)
        {
            var airportJson = new StringContent(
                JsonSerializer.Serialize(airport),
                Encoding.UTF8,
                Application.Json);
            using var httpResponseMessage = await _httpClient.PostAsync("/api/airport", airportJson);

            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadAsStringAsync();

        }

        #endregion

        #region Airline

        public async Task<IEnumerable<string>> GetAirlines()
            => await _httpClient.GetFromJsonAsync<IEnumerable<string>>("/api/airline");

        public async Task<string> CreateAirline(AirlaneRequestModel airlie)
        {
            var airlineJson = new StringContent(
                JsonSerializer.Serialize(airlie),
                Encoding.UTF8,
                Application.Json);
            using var httpResponseMessage = await _httpClient.PostAsync("/api/airline", airlineJson);

            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadAsStringAsync();

        }

        #endregion

        #region Flight

        public async Task<IEnumerable<string>> GetFlights()
            => await _httpClient.GetFromJsonAsync<IEnumerable<string>>("/api/flight");

        public async Task<string> GetAviableFlights(FindFlightRequestModel model)
        {
            var modelAsJson = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                Application.Json);
            var newUrl = _webApiUrl + "/api/aviableflights";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUrl),
                Content = modelAsJson
            };

            using var httpResponseMessage = await _httpClient.SendAsync(request);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateFlight(FlightRequestModel flight)
        {
            var flightJson = new StringContent(
                JsonSerializer.Serialize(flight),
                Encoding.UTF8,
                Application.Json);
            using var httpResponseMessage = await _httpClient.PostAsync("/api/flight", flightJson);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

        #endregion

        #region SystemDetails

        public async Task<string> GetSystemDetails()
        {
            var response = await _httpClient.GetAsync("/api/system");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        #endregion

        #region Section

        public async Task<string> CreateSection(SectionRequestModel section)
        {
            var sectionJson = new StringContent(
                JsonSerializer.Serialize(section),
                Encoding.UTF8,
                Application.Json);
            using var httpResponseMessage = await _httpClient.PostAsync("/api/section", sectionJson);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

        #endregion

        #region Seat

        public async Task<string> BookSeat(BookSeatRequestModel seat)
        {
            var seatJson = new StringContent(
                JsonSerializer.Serialize(seat),
                Encoding.UTF8,
                Application.Json);
            using var httpResponseMessage = await _httpClient.PutAsync("/api/seat", seatJson);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

        #endregion
    }
}
