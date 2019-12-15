using System.ComponentModel.DataAnnotations;

namespace SmartHome.DeviceService.Dtos
{
    /// <summary>
    ///     Data transfer object for creating a device
    /// </summary>
    public class RegisterDeviceDto
    {
        /// <summary>
        ///     Mac address
        /// </summary>
        [Required]
        public string MacAddress { get; set; }
    }
}
