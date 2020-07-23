namespace SmartHome.Weather
{
    /// <summary>
    ///     Represents the weather settings.
    /// </summary>
    public class WeatherSettings
    {
        /// <summary>
        ///     The open weather api key.
        /// </summary>
        public string OpenWeatherApiKey { get; set; }

        /// <summary>
        ///     <c>True</c> if metric units should be used.
        /// </summary>
        public bool UseMetricUnits { get; set; }

        /// <summary>
        ///     The latitude of the location to determine the weather.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        ///     The longitude of the location to determine the weather.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        ///     The language code for the weather data.
        /// </summary>
        public string LanguageCode { get; set; }
    }
}