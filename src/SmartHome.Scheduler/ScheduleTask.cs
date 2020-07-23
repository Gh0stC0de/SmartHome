using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SmartHome.Scheduler
{
    /// <summary>
    ///     A task to execute scheduled.
    /// </summary>
    public class ScheduleTask : ScheduledProcessor
    {
        private readonly ILogger<ScheduleTask> _logger;

        public ScheduleTask(ILogger<ScheduleTask> logger, IServiceScopeFactory serviceScopeFactory) : base(
            serviceScopeFactory, logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        protected override string Schedule => "0 6 * * *";

        /// <inheritdoc />
        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            _logger.LogTrace("Processing in scope.");

            // Process something in scope.

            _logger.LogDebug("Finished.");
            return Task.CompletedTask;
        }
    }
}