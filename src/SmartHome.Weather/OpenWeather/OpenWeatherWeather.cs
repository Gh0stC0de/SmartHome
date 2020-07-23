using System.Text.Json.Serialization;

namespace SmartHome.Weather.OpenWeather
{
    /// <summary>
    ///     An open weather weather data.
    /// </summary>
    public class OpenWeatherWeather
    {
        /// <summary>
        ///     The date and time of the weather data in UNIX seconds.
        /// </summary>
        [JsonPropertyName("dt")]
        public long DateTime { get; set; }

        /// <summary>
        ///     The sunrise timestamp in UNIX seconds.
        /// </summary>
        [JsonPropertyName("sunrise")]
        public long Sunrise { get; set; }

        /// <summary>
        ///     The sunset timestamp in UNIX seconds.
        /// </summary>
        [JsonPropertyName("sunset")]
        public long Sunset { get; set; }
    }
}