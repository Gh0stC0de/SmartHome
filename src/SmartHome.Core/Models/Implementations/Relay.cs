using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SmartHome.Core.Helper;
using SmartHome.Core.Models.Abstractions;

namespace SmartHome.Core.Models.Implementations
{
    /// <summary>
    ///     Relay button
    /// </summary>
    public class RelayButton : ActorComponent
    {
        /// <summary>
        ///     The delay in seconds
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        ///     Signal pin of the relay
        /// </summary>
        [Required]
        public int PinNumber { get; set; }


        /// <inheritdoc />
        public override async void Execute(Device device)
        {
            await PressButton(device, PinNumber, Delay);
        }

        /// <summary>
        ///     Presses the relay button on the device
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="pinNumber">Pin number of the relay signal</param>
        /// <param name="delay">Relay delay</param>
        private async Task PressButton(Device device, int pinNumber, int delay)
        {
            var uri = $"https://{device.IPv4Address}/relay?pin={pinNumber}&delay={delay}";

            await WebHelper.PostAsync(uri, string.Empty, string.Empty);
        }
    }

    /// <summary>
    ///     Relay toggle
    /// </summary>
    public class RelayToggle : ActorComponent
    {
        /// <summary>
        ///     Signal pin of the relay
        /// </summary>
        [Required]
        public int PinNumber { get; set; }

        /// <summary>
        ///     <c>True</c> if the <see cref="RelayToggle"/> is toggled
        /// </summary>
        public bool IsToggled { get; set; }

        /// <inheritdoc />
        public override async void Execute(Device device)
        {
            await ToggleSwitch(device, PinNumber);
        }

        /// <summary>
        ///     Toggles the switch on a device
        /// </summary>
        /// <param name="device">Device</param>
        /// <param name="pinNumber">Pin number of the relay signal</param>
        private async Task ToggleSwitch(Device device, int pinNumber)
        {
            var newState = IsToggled ? 0 : 1;
            var uri = $"https://{device.IPv4Address}/relay?pin={pinNumber}&toggle={newState}";

            await WebHelper.PostAsync(uri, string.Empty, string.Empty);
        }
    }
}