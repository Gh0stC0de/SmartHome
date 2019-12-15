using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Models;
using SmartHome.Core.Models.Implementations;

namespace SmartHome.Infrastructure.DbContexts.Implementations
{
    /// <summary>
    ///     Database context for smart home
    /// </summary>
    [Obsolete("Use 'SmartHomeDbContext' instead.")]
    public class SmartHomeContext : IdentityDbContext
    {
        /// <inheritdoc />
        public SmartHomeContext(DbContextOptions<SmartHomeContext> options)
            : base(options)
        {            
        }

        /// <inheritdoc />
        public SmartHomeContext()
        {
        }

        /// <summary>
        ///     Devices table
        /// </summary>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        ///     Relay button tables
        /// </summary>
        public DbSet<RelayButton> RelayButtons { get; set; }

        /// <summary>
        ///     Relay toggles table
        /// </summary>
        public DbSet<RelayToggle> RelaySwitches { get; set; }
    }
}
