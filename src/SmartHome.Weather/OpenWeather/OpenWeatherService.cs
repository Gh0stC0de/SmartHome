using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SmartHome.Weather.Services;

namespace SmartHome.Weather.OpenWeather
{
    /// <summary>
    ///     Represents an open weather weather service
    /// </summary>
    public class OpenWeatherService : IWeatherService
    {
        private readonly IConfiguration _configuration;

        public OpenWeatherService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public DateTime NextSunrise()
        {
            var weather = GetWeather();
            var sunriseToday = DateTimeOffset.FromUnixTimeSeconds(weather.CurrentWeather.Sunrise).LocalDateTime;

            return sunriseToday > DateTime.Now
                ? sunriseToday
                : DateTimeOffset.FromUnixTimeSeconds(weather.DailyWeather.Skip(1).First().Sunrise).LocalDateTime;
        }

        /// <inheritdoc />
        public DateTime NextSunset()
        {
            var weather = GetWeather();
            var sunsetToday = DateTimeOffset.FromUnixTimeSeconds(weather.CurrentWeather.Sunset).LocalDateTime;

            return sunsetToday > DateTime.Now
                ? sunsetToday
                : DateTimeOffset.FromUnixTimeSeconds(weather.DailyWeather.Skip(1).First().Sunset).LocalDateTime;
        }

        /// <summary>
        ///     Gets the weather.
        /// </summary>
        /// <returns>The weather.</returns>
        private OpenWeatherOneCallResponse GetWeather()
        {
            var settings = _configuration.GetSection("WeatherSettings").Get<WeatherSettings>();
            var unit = settings.UseMetricUnits ? "metric" : "imperial";
            var client = new HttpClient();
            var response = client
                .GetAsync($"https://api.openweathermap.org/data/2.5/onecall?lat={settings.Latitude}&lon={settings.Longitude}&units={unit}&lang={settings.LanguageCode}&appid={settings.OpenWeatherApiKey}")
                .Result;
            var json = "";
            if (response.IsSuccessStatusCode) json = response.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<OpenWeatherOneCallResponse>(json);
        }
    }
}