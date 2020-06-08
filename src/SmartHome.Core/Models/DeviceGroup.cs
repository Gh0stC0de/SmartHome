using System;

namespace SmartHome.Core.Models
{
    /// <summary>
    ///     Represents relationships of devices and groups.
    /// </summary>
    public class DeviceGroup
    {
        /// <summary>
        ///     The device identifier.
        /// </summary>
        public Guid DeviceId { get; set; }

        /// <summary>
        ///     The device.
        /// </summary>
        public Device Device { get; set; }

        /// <summary>
        ///     The group identifier.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        ///     The group.
        /// </summary>
        public Group Group { get; set; }
    }
}