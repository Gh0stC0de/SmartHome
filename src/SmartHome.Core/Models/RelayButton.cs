using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SmartHome.Core.Helper;

namespace SmartHome.Core.Models
{
    /// <summary>
    ///     Relay button
    /// </summary>
    public class RelayButton
    {
        /// <summary>
        ///     Identity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Signal pin of the relay
        /// </summary>
        [Required]
        public int PinNumber { get; set; }

        /// <summary>
        ///     Presses the relay button on the device
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="pinNumber">Pin number of the relay signal</param>
        /// <param name="delay">Relay delay</param>
        public async Task PressButton(Device device, int delay)
        {
            var uri = $"http://{device.IPv4Address}/relay?pin={PinNumber}&delay={delay}";

            await WebHelper.PostAsync(uri, string.Empty, string.Empty);
        }
    }
}