using System.ComponentModel.DataAnnotations;

namespace SmartHome.Service.Models.User
{
    /// <summary>
    ///     Represents a user registration.
    /// </summary>
    public class UserRegistration
    {
        /// <summary>
        ///     Username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        ///     Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}