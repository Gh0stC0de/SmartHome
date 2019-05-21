using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Models;

namespace SmartHome.Infrastructure.DbContexts
{
    /// <summary>
    ///     Database context for smart home
    /// </summary>
    public class SmartHomeContext : DbContext
    {
        /// <inheritdoc />
        public SmartHomeContext(DbContextOptions options)
            : base(options)
        {            
        }

        /// <summary>
        ///     Devices table
        /// </summary>
        public DbSet<Device> Devices { get; set; }
    }
}
