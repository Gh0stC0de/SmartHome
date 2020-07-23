using System;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SmartHome.Weather.OpenWeather;

namespace SmartHome.Weather.Tests
{
    public class WeatherServiceTest
    {
        public IConfiguration Configuration { get; set; }

        [SetUp]
        public void Setup()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.test.json");
            configurationBuilder.AddUserSecrets<WeatherServiceTest>();
            Configuration = configurationBuilder.Build();
        }

        [Test]
        public void NextSunriseShouldNotBeDateTimeMinValue()
        {
            var service = new OpenWeatherService(Configuration);

            var sunrise = service.NextSunrise();

            Assert.AreNotEqual(DateTime.MinValue, sunrise);
        }
    }
}