using System;

namespace SmartHome.Service.Models.Device
{
    /// <summary>
    ///     Represents a device response.
    /// </summary>
    public class DeviceResponse
    {
        /// <summary>
        ///     Device identity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Device name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Device mac address
        /// </summary>
        public string MacAddress { get; set; }
    }
}