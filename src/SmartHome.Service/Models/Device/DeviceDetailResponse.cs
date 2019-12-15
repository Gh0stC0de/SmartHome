using System;

namespace SmartHome.Service.Models.Device
{
    /// <summary>
    ///     Represents a device detail response.
    /// </summary>
    public class DeviceDetailResponse
    {
        /// <summary>
        ///     Identity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Token to access device
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     Device name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Device description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Device location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        ///     Internet protocol v4 address
        /// </summary>
        public string IPv4Address { get; set; }

        /// <summary>
        ///     Mac address
        /// </summary>
        public string MacAddress { get; set; }
    }
}