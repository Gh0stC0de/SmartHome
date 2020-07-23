using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SmartHome.Scheduler
{
    /// <summary>
    ///     A processor that processes a task in scope.
    /// </summary>
    public abstract class ScopedProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ScopedProcessor(IServiceScopeFactory serviceScopeFactory, ILogger<ScopedProcessor> logger)
            : base(logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <inheritdoc />
        protected override async Task Process()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                await ProcessInScope(scope.ServiceProvider);
            }
        }

        /// <summary>
        ///     Processes something in scope.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public abstract Task ProcessInScope(IServiceProvider serviceProvider);
    }
}