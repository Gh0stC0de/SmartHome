using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SmartHome.Core.Models.Abstractions;

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
        public int Id { get; set; }

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
        ///     <c>True</c> if the device is authenticated
        /// </summary>
        [DefaultValue(false)]
        public bool IsAuthenticated { get; set; }

        /// <summary>
        ///     Actor components
        /// </summary>
        public virtual List<ActorComponent> ActorComponents { get; set; }
    }
}
