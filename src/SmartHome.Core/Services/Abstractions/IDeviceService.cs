using System;
using System.Collections.Generic;
using SmartHome.Core.Exceptions;
using SmartHome.Core.Models;

namespace SmartHome.Core.Services.Abstractions
{
    /// <summary>
    ///     Service for interaction with devices.
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        ///     Registers a device.
        /// </summary>
        /// <param name="macAddress">The mac address of the device</param>
        /// <param name="ipv4Address">The IPv4 address of the device</param>
        /// <returns>The registered device</returns>
        /// <exception cref="ArgumentException">The argument is invalid.</exception>
        /// <exception cref="InvalidOperationException">The device is already registered.</exception>
        Device RegisterDevice(string macAddress, string ipv4Address);

        /// <summary>
        ///     Gets all devices.
        /// </summary>
        /// <returns>List of all devices.</returns>
        List<Device> GetAll();

        /// <summary>
        ///     Gets a device by the identity.
        /// </summary>
        /// <param name="id">The device identity</param>
        /// <returns>The device</returns>
        /// <exception cref="ArgumentException">The argument is invalid.</exception>
        Device Get(Guid? id);

        /// <summary>
        ///     Authenticates a device.
        /// </summary>
        /// <param name="macAddress">The mac address</param>
        /// <param name="ipv4Address">The IPv4 address</param>
        /// <returns>The device authentication response</returns>
        /// <exception cref="ArgumentException">The argument is invalid.</exception>
        /// <exception cref="InvalidCredentialsException">The credentials are invalid.</exception>
        DeviceAuthenticationResponse Authenticate(string macAddress, string ipv4Address);
    }
}