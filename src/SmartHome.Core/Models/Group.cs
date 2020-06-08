using System;
using System.Collections.Generic;

namespace SmartHome.Core.Models
{
    /// <summary>
    ///     Represents a group.
    /// </summary>
    public class Group
    {
        /// <summary>
        ///     Identity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Represents the relation between the devices and groups.
        /// </summary>
        public List<DeviceGroup> DeviceGroups { get; set; }
    }
}