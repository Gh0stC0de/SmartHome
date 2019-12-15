using System.ComponentModel.DataAnnotations;

namespace SmartHome.Service.Models.Device
{
    /// <summary>
    ///     Represents a device registration.
    /// </summary>
    public class DeviceRegistration
    {
        /// <summary>
        ///     Mac address of the device.
        /// </summary>
        [Required]
        public string MacAddress { get; set; }
    }
}