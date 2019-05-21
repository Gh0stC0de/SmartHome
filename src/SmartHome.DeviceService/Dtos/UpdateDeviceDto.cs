using System.ComponentModel.DataAnnotations;

namespace SmartHome.DeviceService.Dtos
{
    /// <summary>
    ///     Data transfer model for updating a device
    /// </summary>
    public class UpdateDeviceDto
    {
        /// <summary>
        ///     Identity
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        ///     Token to access device
        /// </summary>
        public string AccessToken { get; set; }
    }
}
