using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using ABS_Models;
using static ABS_DataConstants.DataConstrain;

using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;

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
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Airport

        public async Task<IEnumerable<string>> GetAirports() => await _httpClient.GetFromJsonAsync<IEnumerable<string>>(AIRPORT_API_PATH);


        public async Task<string> CreateAirport(AirportModel airport)
        {
            try
            {
                var airportJson = new StringContent(
                    JsonSerializer.Serialize(airport),
                    Encoding.UTF8,
                    Application.Json);
                using var httpResponseMessage = await _httpClient.PostAsync(AIRPORT_API_PATH, airportJson);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    return await GetErrorsFromHttpResponse(httpResponseMessage);
                }

                return await httpResponseMessage.Content.ReadFromJsonAsync<string>();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        #endregion

        #region Airline

        public async Task<IEnumerable<string>> GetAirlines()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<string>>(AIRLINE_API_PATH);
        }

        public async Task<string> CreateAirline(AirlineModel airlie)
        {
            try
            {
                var airlineJson = new StringContent(
                    JsonSerializer.Serialize(airlie),
                    Encoding.UTF8,
                    Application.Json);
                using var httpResponseMessage = await _httpClient.PostAsync(AIRLINE_API_PATH, airlineJson);

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
            => await _httpClient.GetFromJsonAsync<IEnumerable<string>>(FLIGHT_API_PATH);

        public async Task<string> GetAviableFlights(AviableFlightsModel flight)
        {
            try
            {
                var modelAsJson = new StringContent(
                    JsonSerializer.Serialize(flight),
                    Encoding.UTF8,
                    Application.Json);
                var newUrl = _webApiUrl + FIND_FLIGHT_API_PATH;

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

                return await httpResponseMessage.Content.ReadFromJsonAsync<string>();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> CreateFlight(FlightModel flight)
        {
            try
            {
                var flightJson = new StringContent(
                    JsonSerializer.Serialize(flight),
                    Encoding.UTF8,
                    Application.Json);
                using var httpResponseMessage = await _httpClient.PostAsync(FLIGHT_API_PATH, flightJson);

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
                var response = await _httpClient.GetAsync(SUSTEM_API_PATH);

                if (!response.IsSuccessStatusCode)
                {
                    return await GetErrorsFromHttpResponse(response);
                }

                var content = await response.Content.ReadFromJsonAsync<string>();
                
                return content;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region Section

        public async Task<string> CreateSection(FlightSectionModel section)
        {
            try
            {
                var sectionJson = new StringContent(
                    JsonSerializer.Serialize(section),
                    Encoding.UTF8,
                    Application.Json);
                using var httpResponseMessage = await _httpClient.PostAsync(SECTION_API_PATH, sectionJson);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    return await GetErrorsFromHttpResponse(httpResponseMessage);
                }

                return await httpResponseMessage.Content.ReadFromJsonAsync<string>();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region Seat

        public async Task<string> BookSeat(BookSeatModel seat)
        {
            try
            {
                var seatJson = new StringContent(
                    JsonSerializer.Serialize(seat),
                    Encoding.UTF8,
                    Application.Json);
                using var httpResponseMessage = await _httpClient.PutAsync(SEAT_API_PATH, seatJson);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    return await GetErrorsFromHttpResponse(httpResponseMessage);
                }

                return await httpResponseMessage.Content.ReadFromJsonAsync<string>();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        private async Task<string> GetErrorsFromHttpResponse(HttpResponseMessage httpResponseMessage)
        {
            ErrorHttpModel reposnse;
            try
            {
                reposnse = await httpResponseMessage.Content.ReadFromJsonAsync<ErrorHttpModel>();
            }
            catch (Exception)
            {
                var result= await httpResponseMessage.Content.ReadAsStringAsync();
                return result;
            }
            var sb = new StringBuilder();
            foreach (var error in reposnse.Errors)
            {
                sb.AppendLine(error.Value[0]);
            }
            return sb.ToString().Trim();
        }
    }
}
