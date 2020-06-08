using System;
using System.Linq;
using SmartHome.Core.Models;
using SmartHome.Infrastructure.DbContexts.Implementations;

namespace SmartHome.Infrastructure.DbContexts
{
    /// <summary>
    ///     Extensions for <see cref="SmartHomeContext"/>
    /// </summary>
    public static class SmartHomeContextExtensions
    {
        /// <summary>
        ///     Seeds the context
        /// </summary>
        /// <param name="context"><see cref="SmartHomeContext"/> context</param>
        public static void Seed(this SmartHomeContext context)
        {
            if (!context.Devices.Any())
            {
                context.Devices.Add(new Device
                {
                    Name = "Test device",
                    AccessToken = "TestAccessToken",
                    Description = "Test description",
                    IPv4Address = "127.0.0.1",
                    Location = "Local",
                    MacAddress = "000:000:000:000"
                });

                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}