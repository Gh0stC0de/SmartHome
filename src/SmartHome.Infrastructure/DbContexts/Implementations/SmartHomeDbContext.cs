using System;
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

        /// <inheritdoc />
        public DbSet<RelayButton> RelayButtons { get; set; }

        /// <inheritdoc />
        public DbSet<Shutter> Shutters { get; set; }

        /// <inheritdoc />
        public DbSet<Group> Groups { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceGroup>()
                .HasKey(deviceGroup => new {deviceGroup.DeviceId, deviceGroup.GroupId});

            modelBuilder.Entity<DeviceGroup>()
                .HasOne(dg => dg.Device)
                .WithMany(device => device.DeviceGroups)
                .HasForeignKey(dg => dg.DeviceId);

            modelBuilder.Entity<DeviceGroup>()
                .HasOne(dg => dg.Group)
                .WithMany(group => group.DeviceGroups)
                .HasForeignKey(dg => dg.GroupId);

            modelBuilder.Entity<Group>().HasData(new Group
                {Id = Guid.NewGuid(), Name = Core.Constants.Groups.SunriseTask});
            modelBuilder.Entity<Group>().HasData(new Group
                {Id = Guid.NewGuid(), Name = Core.Constants.Groups.SunsetTask});
        }
    }
}