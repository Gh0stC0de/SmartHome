using System;

namespace SmartHome.Core.Models
{
    /// <summary>
    ///     Represents a user.
    /// </summary>
    public class User
    {
        /// <summary>
        ///     Identity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Password hash
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        ///     Password salt
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        ///     <c>True</c> if the user is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}