using System;
using System.Collections.Generic;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Core.Exceptions;
using SmartHome.Core.Extensions;
using SmartHome.Core.Models;
using SmartHome.Core.Services.Abstractions;
using SmartHome.Service.Models;
using SmartHome.Service.Models.Device;
using DeviceAuthenticationResponse = SmartHome.Service.Models.Device.DeviceAuthenticationResponse;

namespace SmartHome.Service.Controllers
{
    /// <summary>
    ///     Controls devices.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DeviceController" /> with a device service.
        /// </summary>
        /// <param name="deviceService">The device service.</param>
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        /// <summary>
        ///     Registers a device.
        /// </summary>
        /// <param name="model">Device registration model</param>
        /// <returns>Action result</returns>
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        [ProducesResponseType(typeof(DeviceRegistrationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] DeviceRegistration model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var ipv4 = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            Device device;
            try
            {
                device = _deviceService.RegisterDevice(model.MacAddress, ipv4);
            }
            catch (Exception e) when (e is ArgumentException || e is InvalidOperationException)
            {
                return BadRequest(e.Adapt<BadRequestResponse>());
            }

            return Ok(device.Adapt<DeviceRegistrationResponse>());
        }

        /// <summary>
        ///     Authenticates a device.
        /// </summary>
        /// <param name="model">The device authentication</param>
        [AllowAnonymous]
        [Route("Authenticate")]
        [HttpPost]
        [ProducesResponseType(typeof(DeviceAuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Authenticate([FromBody] DeviceAuthentication model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var ipv4 = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            Core.Models.DeviceAuthenticationResponse authenticationResponse;
            try
            {
                authenticationResponse = _deviceService.Authenticate(model.MacAddress, ipv4);
            }
            catch (InvalidCredentialsException e)
            {
                return BadRequest(e.Adapt<BadRequestResponse>());
            }

            return Ok(authenticationResponse.Adapt<DeviceAuthenticationResponse>());
        }

        /// <summary>
        ///     Gets all devices.
        /// </summary>
        /// <returns>All devices</returns>
        /// <example>GET: api/Device</example>
        [HttpGet]
        [ProducesResponseType(typeof(List<DeviceResponse>), StatusCodes.Status200OK)]
        public IActionResult GetDevices()
        {
            var devices = _deviceService.GetAll();
            return Ok(devices.Adapt<List<DeviceResponse>>());
        }

        /// <summary>
        ///     Gets a device by the identity.
        /// </summary>
        /// <param name="id">The device identity</param>
        /// <returns>The device</returns>
        /// <example>GET: api/Device/5</example>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeviceDetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDevice(Guid? id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest(new ArgumentException("The value can not be null or empty.", nameof(id))
                    .Adapt<BadRequestResponse>());

            var device = _deviceService.Get(id);

            if (device == null) return NotFound();

            return Ok(device.Adapt<DeviceDetailResponse>());
        }
    }
}