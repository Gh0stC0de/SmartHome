using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NCrontab;

namespace SmartHome.Scheduler
{
    /// <summary>
    ///     A processor that processes a scheduled task.
    /// </summary>
    public abstract class ScheduledProcessor : ScopedProcessor
    {
        private const int MinSleepMilliseconds = 250;
        private readonly ILogger<ScheduledProcessor> _logger;
        private readonly CrontabSchedule _schedule;
        private DateTime _nextRun;

        public ScheduledProcessor(IServiceScopeFactory serviceScopeFactory, ILogger<ScheduledProcessor> logger)
            : base(serviceScopeFactory, logger)
        {
            _logger = logger;
            _schedule = CrontabSchedule.Parse(Schedule);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        /// <summary>
        ///     The schedule as cron expression.
        /// </summary>
        protected abstract string Schedule { get; }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                if (now > _nextRun)
                {
                    await Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
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