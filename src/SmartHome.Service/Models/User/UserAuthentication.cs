using System.ComponentModel.DataAnnotations;

namespace SmartHome.Service.Models.User
{
    /// <summary>
    ///     Represents a model for authentication.
    /// </summary>
    public class UserAuthentication
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