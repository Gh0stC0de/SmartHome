using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using SmartHome.Core.Exceptions;
using SmartHome.Core.Extensions;
using SmartHome.Core.Models;
using SmartHome.Core.Services.Abstractions;
using SmartHome.Infrastructure.DbContexts.Abstractions;

namespace SmartHome.Infrastructure.Services.Implementations
{
    /// <inheritdoc />
    public class DeviceService : IDeviceService
    {
        private readonly ISmartHomeDbContext _context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DeviceService" /> class with a context.
        /// </summary>
        /// <param name="context">The smart home db context</param>
        public DeviceService(ISmartHomeDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public Device RegisterDevice(string macAddress, string ipv4Address)
        {
            if (string.IsNullOrWhiteSpace(macAddress))
                throw new ArgumentException("The value can not be null, empty or white space.", nameof(macAddress));
            if (string.IsNullOrWhiteSpace(ipv4Address))
                throw new ArgumentException("The value can not be null, empty or white space.", nameof(ipv4Address));
            if (DeviceExists(macAddress))
                throw new InvalidOperationException($"The device '{macAddress}' is already registered.");

            var device = _context.Devices.Add(new Device
            {
                Id = Guid.NewGuid(),
                MacAddress = macAddress,
                IPv4Address = ipv4Address
            });
            _context.SaveChanges();

            return device.Entity;
        }

        /// <inheritdoc />
        public List<Device> GetAll()
        {
            return _context.Devices.ToList();
        }

        /// <inheritdoc />
        public Device Get(Guid? id)
        {
            if (id.IsNullOrEmpty())
                throw new ArgumentException("The value can not be null or empty.", nameof(id));

            return _context.Devices.Find(id);
        }

        /// <inheritdoc />
        public DeviceAuthenticationResponse Authenticate(string macAddress, string ipv4Address)
        {
            if (string.IsNullOrWhiteSpace(macAddress))
                throw new ArgumentException("The value can not be null, empty or white space.", nameof(macAddress));
            if (string.IsNullOrWhiteSpace(ipv4Address))
                throw new ArgumentException("The value can not be null, empty or white space.", nameof(ipv4Address));
            
            var device = _context.Devices.FirstOrDefault(d => d.MacAddress == macAddress);

            if (device == null)
                throw new InvalidCredentialsException("The device does not exist.");

            device.IPv4Address = ipv4Address;
            _context.SaveChanges();

            return device.Adapt<DeviceAuthenticationResponse>();
        }

        /// <summary>
        ///     Checks if a device exists by the mac address.
        /// </summary>
        /// <param name="macAddress">The mac address</param>
        /// <returns><b>True</b> if the device exists</returns>
        private bool DeviceExists(string macAddress)
        {
            return _context.Devices.Any(e => e.MacAddress == macAddress);
        }
    }
}