using System;
using Mapster;
using SmartHome.Core.Models;
using SmartHome.Service.Models;
using SmartHome.Service.Models.Device;
using SmartHome.Service.Models.User;
using DeviceAuthenticationResponse = SmartHome.Core.Models.DeviceAuthenticationResponse;
using UserAuthenticationResponse = SmartHome.Core.Models.UserAuthenticationResponse;

namespace SmartHome.Service.Helpers
{
    /// <summary>
    ///     Helper class for <see cref="Mapster"/> mappings.
    /// </summary>
    public static class MappingHelper
    {
        /// <summary>
        ///     Configures mappings
        /// </summary>
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<Exception, BadRequestResponse>
                .NewConfig()
                .Map(dest => dest.Title, src => src.GetType().Name)
                .Map(dest => dest.Detail, src => src.Message);
            TypeAdapterConfig<User, UserRegistrationResponse>
                .NewConfig()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.Username, src => src.Username);
            TypeAdapterConfig<User, UserAuthenticationResponse>
                .NewConfig()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.Username, src => src.Username);
            TypeAdapterConfig<Device, DeviceRegistrationResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.MacAddress, src => src.MacAddress);
            TypeAdapterConfig<Device, DeviceAuthenticationResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id);
            TypeAdapterConfig<Device, DeviceResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.MacAddress, src => src.MacAddress);
            TypeAdapterConfig<Device, DeviceDetailResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.AccessToken, src => src.AccessToken)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Location, src => src.Location)
                .Map(dest => dest.IPv4Address, src => src.IPv4Address)
                .Map(dest => dest.MacAddress, src => src.MacAddress);
        }
    }
}