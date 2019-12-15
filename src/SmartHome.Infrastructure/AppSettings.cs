namespace SmartHome.Infrastructure
{
    /// <summary>
    ///     Represents application settings
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        ///     Secret to sign jason web tokens
        /// </summary>
        public string TokenSecret { get; set; }

        /// <summary>
        ///     Token expiration in days
        /// </summary>
        public int TokenExpirationInDays { get; set; }
    }
}