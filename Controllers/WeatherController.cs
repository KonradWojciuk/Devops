using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["WeatherApiKey"];
        }

        public async Task<WeatherForecast?> GetWeatherAsync(string city)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric&lang=pl");
            response.EnsureSuccessStatusCode();

            if (string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
            {
                return null;
            }

            var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(response);
            return weatherData;
        }
    }
}
