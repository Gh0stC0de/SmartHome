using System;
using System.Security.Authentication;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Core.Models;
using SmartHome.Core.Services.Abstractions;
using SmartHome.Service.Models;
using SmartHome.Service.Models.User;
using UserAuthenticationResponse = SmartHome.Service.Models.User.UserAuthenticationResponse;

namespace SmartHome.Service.Controllers
{
    /// <summary>
    ///     Controls interaction with user.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserController" /> class with a user service.
        /// </summary>
        /// <param name="authenticationService">The authentication service</param>
        public UserController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        ///     Authenticates a user.
        /// </summary>
        /// <param name="model">User authentication model</param>
        /// <returns>Action result</returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(typeof(UserAuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Authenticate([FromBody] UserAuthentication model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            Core.Models.UserAuthenticationResponse authenticationResponse;
            try
            {
                authenticationResponse = _authenticationService.Authenticate(model.Username, model.Password);
            }
            catch (InvalidCredentialException e)
            {
                return BadRequest(e.Adapt<BadRequestResponse>());
            }

            return Ok(authenticationResponse.Adapt<UserAuthenticationResponse>());
        }

        /// <summary>
        ///     Registers a user.
        /// </summary>
        /// <param name="model">The user register</param>
        /// <returns>Action result</returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserRegistrationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] UserRegistration model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var user = model.Adapt<User>();

            User createdUser;
            try
            {
                createdUser = _authenticationService.Register(user, model.Password);
            }
            catch (Exception e) when (e is ArgumentNullException || e is ArgumentException ||
                                      e is InvalidOperationException)
            {
                return BadRequest(e.Adapt<BadRequestResponse>());
            }

            return Ok(createdUser.Adapt<UserRegistrationResponse>());
        }
    }
}