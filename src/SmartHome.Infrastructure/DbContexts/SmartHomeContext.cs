using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Models;

namespace SmartHome.Infrastructure.DbContexts
{
    /// <summary>
    ///     Database context for smart home
    /// </summary>
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
    }
}
