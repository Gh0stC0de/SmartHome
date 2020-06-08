using System;

namespace SmartHome.Core.Models
{
    /// <summary>
    ///     Represents a shutter.
    /// </summary>
    public class Shutter
    {
        /// <summary>
        ///     Identity
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        ///     Time to open the shutter in milliseconds
        /// </summary>
        public int TimeToOpen { get; set; }

        /// <summary>
        ///     Button to open the shutter
        /// </summary>
        public RelayButton OpenButton { get; set; }

        /// <summary>
        ///     Button to close the shutter
        /// </summary>
        public RelayButton CloseButton { get; set; }

        /// <summary>
        ///     Device connected to the shutter
        /// </summary>
        public Device Device { get; set; }
    }
}