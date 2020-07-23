using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartHome.Scheduler;
using SmartHome.Weather.Services;

namespace SmartHome.Weather.Scheduler
{
    /// <summary>
    ///     A processor that processes a task at sunset.
    /// </summary>
    public abstract class SunsetProcessor : ScopedProcessor
    {
        private const int MinSleepMilliseconds = 250;
        private readonly IWeatherService _weatherService;
        private readonly ILogger<SunsetProcessor> _logger;
        private DateTime _nextRun;

        public SunsetProcessor(IWeatherService weatherService
            , IServiceScopeFactory serviceScopeFactory
            , ILogger<SunsetProcessor> logger)
            : base(serviceScopeFactory, logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        /// <inheritdoc />
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _nextRun = _weatherService.NextSunset();
            _logger.LogDebug($"Next run: {_nextRun}");
            return base.StartAsync(cancellationToken);
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                if (now > _nextRun)
                {
                    await Process();
                    _nextRun = _weatherService.NextSunset();
                    _logger.LogDebug($"Next run: {_nextRun}");
                }

                var delay = Math.Round((_nextRun - now).TotalMilliseconds / 2);
                delay = delay > MinSleepMilliseconds ? delay : MinSleepMilliseconds;

                _logger.LogDebug($"Delay: {delay}");
                await Task.Delay(TimeSpan.FromMilliseconds(delay), stoppingToken);
            } while (!stoppingToken.IsCancellationRequested);
        }
    }
}