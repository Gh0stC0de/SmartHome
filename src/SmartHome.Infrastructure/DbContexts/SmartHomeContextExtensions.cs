using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using SmartHome.Core.Models;
using SmartHome.Core.Models.Abstractions;
using SmartHome.Core.Models.Implementations;

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
                    IsAuthenticated = false,
                    Location = "Local",
                    MacAddress = "000:000:000:000",
                    ActorComponents = new List<ActorComponent>
                    {
                        new RelayButton
                        {
                            Name = "Test RelayButton",
                            PinNumber = 1,
                            Delay = 10
                        },
                        new RelayToggle
                        {
                            Name = "Test Switch",
                            PinNumber = 2
                        }
                    }
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