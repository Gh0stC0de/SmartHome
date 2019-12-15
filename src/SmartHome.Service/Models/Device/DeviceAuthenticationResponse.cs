using System;

namespace SmartHome.Service.Models.Device
{
    /// <summary>
    ///     Represents a device authentication response.
    /// </summary>
    public class DeviceAuthenticationResponse
    {
        /// <summary>
        ///     The device identity
        /// </summary>
        public Guid Id { get; set; }
    }
}