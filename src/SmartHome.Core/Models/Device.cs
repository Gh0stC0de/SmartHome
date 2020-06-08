using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartHome.Core.Models
{
    /// <summary>
    ///     Device model
    /// </summary>
    public class Device
    {
        /// <summary>
        ///     Identity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Token to access device
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     Device name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Device description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Device location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        ///     Internet protocol v4 address
        /// </summary>
        [Required]
        public string IPv4Address { get; set; }

        /// <summary>
        ///     Mac address
        /// </summary>
        [Required]
        public string MacAddress { get; set; }

        /// <summary>
        ///     Represents the relation between the devices and groups.
        /// </summary>
        public List<DeviceGroup> DeviceGroups { get; set; }
    }
}