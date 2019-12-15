using System;
using SmartHome.Core.Exceptions;
using SmartHome.Core.Models;

namespace SmartHome.Core.Services.Abstractions
{
    /// <summary>
    ///     Service for user authentication.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        ///     Authenticates a user.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>User authentication response</returns>
        /// <exception cref="InvalidCredentialsException">The credentials (username and/or password) are invalid.</exception>
        /// <exception cref="InvalidOperationException">There is more than one user with this username.</exception>
        UserAuthenticationResponse Authenticate(string username, string password);

        /// <summary>
        ///     Registers a user.
        /// </summary>
        /// <param name="user">The user to register</param>
        /// <param name="password">The password</param>
        /// <returns>The created user</returns>
        /// <exception cref="ArgumentException">An argument is invalid.</exception>
        /// <exception cref="ArgumentNullException">An argument is null.</exception>
        /// <exception cref="InvalidOperationException">The username is already taken.</exception>
        User Register(User user, string password);
    }
}