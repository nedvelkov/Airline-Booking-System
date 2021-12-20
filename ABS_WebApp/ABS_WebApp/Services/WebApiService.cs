using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using ABS_WebApp.Services.RequestModels;
using static ABS_DataConstants.DataConstrain;

using static System.Net.Mime.MediaTypeNames;

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
            => await _httpClient.GetFromJsonAsync<IEnumerable<string>>(airportApi);

        public async Task<string> CreateAirport(AirportRequestModel airport)
        {
            try
            {
                var airportJson = new StringContent(
                    JsonSerializer.Serialize(airport),
                    Encoding.UTF8,
                    Application.Json);
                using var httpResponseMessage = await _httpClient.PostAsync(airportApi, airportJson);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    return await GetErrorsFromHttpResponse(httpResponseMessage);
                }

                return await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        #endregion

        #region Airline

        public async Task<IEnumerable<string>> GetAirlines()
            => await _httpClient.GetFromJsonAsync<IEnumerable<string>>(airlineApi);

        public async Task<string> CreateAirline(AirlaneRequestModel airlie)
        {
            try
            {
                var airlineJson = new StringContent(
                    JsonSerializer.Serialize(airlie),
                    Encoding.UTF8,
                    Application.Json);
                using var httpResponseMessage = await _httpClient.PostAsync(airlineApi, airlineJson);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    return await GetErrorsFromHttpResponse(httpResponseMessage);
                }

                return await httpResponseMessage.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        #endregion

        #region Flight

        public async Task<IEnumerable<string>> GetFlights()
            => await _httpClient.GetFromJsonAsync<IEnumerable<string>>(flightApi);

        public async Task<string> GetAviableFlights(FindFlightRequestModel model)
        {
            try
            {
                var modelAsJson = new StringContent(
                    JsonSerializer.Serialize(model),
                    Encoding.UTF8,
                    Application.Json);
                var newUrl = _webApiUrl + findFlightApi;

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(newUrl),
                    Content = modelAsJson
                };

                using var httpResponseMessage = await _httpClient.SendAsync(request);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    return await GetErrorsFromHttpResponse(httpResponseMessage);
                }

                return await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> CreateFlight(FlightRequestModel flight)
        {
            try
            {
                var flightJson = new StringContent(
                    JsonSerializer.Serialize(flight),
                    Encoding.UTF8,
                    Application.Json);
                using var httpResponseMessage = await _httpClient.PostAsync(flightApi, flightJson);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    return await GetErrorsFromHttpResponse(httpResponseMessage);
                }

                return await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        #endregion

        #region SystemDetails

        public async Task<string> GetSystemDetails()
        {
            try
            {
                var response = await _httpClient.GetAsync(systemApi);

                if (!response.IsSuccessStatusCode)
                {
                    return await GetErrorsFromHttpResponse(response);
                }

                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region Section

        public async Task<string> CreateSection(SectionRequestModel section)
        {
            try
            {
                var sectionJson = new StringContent(
                    JsonSerializer.Serialize(section),
                    Encoding.UTF8,
                    Application.Json);
                using var httpResponseMessage = await _httpClient.PostAsync(sectionApi, sectionJson);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    return await GetErrorsFromHttpResponse(httpResponseMessage);
                }

                return await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region Seat

        public async Task<string> BookSeat(BookSeatRequestModel seat)
        {
            try
            {
                var seatJson = new StringContent(
                    JsonSerializer.Serialize(seat),
                    Encoding.UTF8,
                    Application.Json);
                using var httpResponseMessage = await _httpClient.PutAsync(seatApi, seatJson);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    return await GetErrorsFromHttpResponse(httpResponseMessage);
                }

                return await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        private async Task<string> GetErrorsFromHttpResponse(HttpResponseMessage httpResponseMessage)
        {
            var reposnse = await httpResponseMessage.Content.ReadFromJsonAsync<ErrorHttpModel>();
            var sb = new StringBuilder();
            foreach (var error in reposnse.Errors)
            {
                sb.AppendLine(error.Value[0]);
            }
            return sb.ToString().Trim();
        }
    }
}
