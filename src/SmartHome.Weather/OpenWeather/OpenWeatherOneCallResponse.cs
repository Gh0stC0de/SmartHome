using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmartHome.Weather.OpenWeather
{
    /// <summary>
    ///     An open weather on call response.
    /// </summary>
    public class OpenWeatherOneCallResponse
    {
        /// <summary>
        ///     The latitude of the weather data location.
        /// </summary>
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        /// <summary>
        ///     The longitude of the weather data location.
        /// </summary>
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        /// <summary>
        ///     The time zone of the weather data location.
        /// </summary>
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        /// <summary>
        ///     The time zone offset of the weather data location.
        /// </summary>
        [JsonPropertyName("timezone_offset")]
        public double TimeZoneOffset { get; set; }

        /// <summary>
        ///     The current weather.
        /// </summary>
        [JsonPropertyName("current")]
        public OpenWeatherWeather CurrentWeather { get; set; }

        /// <summary>
        ///     The daily weather of today and the next 7 days.
        /// </summary>
        [JsonPropertyName("daily")]
        public List<OpenWeatherWeather> DailyWeather { get; set; }
    }
}