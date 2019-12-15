using System.ComponentModel.DataAnnotations;

namespace SmartHome.Service.Models.Device
{
    /// <summary>
    ///     Represents a device authentication.
    /// </summary>
    public class DeviceAuthentication
    {
        /// <summary>
        ///     The mac address
        /// </summary>
        [Required]
        public string MacAddress { get; set; }
    }
}