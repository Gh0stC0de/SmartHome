using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHome.Core.Models.Abstractions
{
    /// <summary>
    ///     Actor component
    /// </summary>
    public abstract class ActorComponent
    {
        /// <summary>
        ///     Identity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Component name
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        ///     Executes actor on device
        /// </summary>
        /// <param name="device">Device to execute on</param>
        public abstract void Execute(Device device);
    }
}