using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Models;
using SmartHome.Infrastructure.DbContexts.Abstractions;

namespace SmartHome.Infrastructure.DbContexts.Implementations
{
    /// <inheritdoc cref="ISmartHomeDbContext" />
    /// <inheritdoc cref="DbContext" />
    public class SmartHomeDbContext : DbContext, ISmartHomeDbContext
    {
        /// <inheritdoc />
        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options)
            : base(options)
        {
        }

        /// <inheritdoc />
        public DbSet<User> Users { get; set; }

        /// <inheritdoc />
        public DbSet<Device> Devices { get; set; }
    }
}