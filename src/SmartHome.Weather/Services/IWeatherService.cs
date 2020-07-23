using System;

namespace SmartHome.Weather.Services
{
    /// <summary>
    ///     Represents a weather service.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        ///     Gets the date and time of the next sunrise.
        /// </summary>
        /// <returns>The date and time of the next sunrise.</returns>
        DateTime NextSunrise();

        /// <summary>
        ///     Gets the date and time of the next sunset.
        /// </summary>
        /// <returns>The date and time of the next sunset.</returns>
        DateTime NextSunset();
    }
}
