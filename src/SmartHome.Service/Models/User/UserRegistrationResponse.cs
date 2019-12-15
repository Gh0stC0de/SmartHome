using System;

namespace SmartHome.Service.Models.User
{
    /// <summary>
    ///     Represents a user registration response.
    /// </summary>
    public class UserRegistrationResponse
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserRegistrationResponse" /> class.
        /// </summary>
        public UserRegistrationResponse()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserRegistrationResponse" /> class with a user identity and a username.
        /// </summary>
        /// <param name="userId">The user identity</param>
        /// <param name="username">The username</param>
        public UserRegistrationResponse(Guid userId, string username)
        {
            UserId = userId;
            Username = username;
        }

        /// <summary>
        ///     User identity
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        ///     Username
        /// </summary>
        public string Username { get; set; }
    }
}