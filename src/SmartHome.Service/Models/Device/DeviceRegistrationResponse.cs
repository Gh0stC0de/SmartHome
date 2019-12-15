using System;

namespace SmartHome.Service.Models.Device
{
    /// <summary>
    ///     Represents a device registration response.
    /// </summary>
    public class DeviceRegistrationResponse
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DeviceRegistrationResponse" /> class.
        /// </summary>
        public DeviceRegistrationResponse()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DeviceRegistrationResponse" /> class with the device identifier and mac
        ///     address.
        /// </summary>
        /// <param name="id">The device identifier</param>
        /// <param name="macAddress">The device mac address</param>
        public DeviceRegistrationResponse(Guid id, string macAddress)
        {
            Id = id;
            MacAddress = macAddress;
        }

        /// <summary>
        ///     The device identity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The mac address of the device
        /// </summary>
        public string MacAddress { get; set; }
    }
}