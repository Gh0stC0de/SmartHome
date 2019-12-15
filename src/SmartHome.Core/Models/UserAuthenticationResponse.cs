using System;

namespace SmartHome.Core.Models
{
    /// <summary>
    ///     Represents an authentication response.
    /// </summary>
    public class UserAuthenticationResponse
    {
        /// <summary>
        ///     Identity
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        ///     Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     API token
        /// </summary>
        public string Token { get; set; }
    }
}