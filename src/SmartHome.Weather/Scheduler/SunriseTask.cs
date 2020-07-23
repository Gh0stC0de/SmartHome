using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartHome.Core.Constants;
using SmartHome.Infrastructure.DbContexts.Abstractions;
using SmartHome.Weather.Services;

namespace SmartHome.Weather.Scheduler
{
    /// <summary>
    ///     A task to execute at sunrise.
    /// </summary>
    public class SunriseTask : SunriseProcessor
    {
        private readonly ILogger<SunriseTask> _logger;
        
        public SunriseTask(ILogger<SunriseTask> logger
            , IServiceScopeFactory serviceScopeFactory
            , IWeatherService weatherService)
            : base(weatherService, serviceScopeFactory, logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            _logger.LogTrace("Processing in scope.");
            var db = serviceProvider.GetService<ISmartHomeDbContext>();

            var devices = db.Groups.Where(g => g.Name == Groups.SunriseTask)
                .SelectMany(g => g.DeviceGroups)
                .Select(group => group.Device)
                .ToList();

            const int pin = 1;
            const int delay = 12500;
            var urls = devices.Select(device => $"http://{device.IPv4Address}/relay?pin={pin}&delay={delay}").ToArray();
            var tasks = new Task[urls.Length];
            for (var i = 0; i < urls.Length; i++)
            {
                var url = urls[i];
                _logger.LogDebug($"[GET] Url: '{url}'");

                tasks[i] = Task.Run(async () =>
                {
                    var request = WebRequest.Create(url);
                    request.Timeout = delay + 5000;
                    try
                    {
                        await request.GetResponseAsync().ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, $"Could not reach '{url}'. Skipping url.", null);
                    }
                });
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
            _logger.LogDebug("Finished processing.");
        }
    }
}