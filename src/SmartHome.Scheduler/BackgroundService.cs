using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SmartHome.Scheduler
{
    /// <summary>
    ///     A background service.
    /// </summary>
    public abstract class BackgroundService : IHostedService
    {
        private readonly ILogger<BackgroundService> _logger;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private Task _executingTask;

        public BackgroundService(ILogger<BackgroundService> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            // If the task is completed then return it,
            // this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted) return _executingTask;

            // Otherwise it's running
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null) return;

            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                    cancellationToken));
            }
        }

        /// <summary>
        ///     Executes the background service.
        /// </summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        protected virtual async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
                _logger.LogDebug("GracePeriod background task is stopping."));

            do
            {
                await Process();

                await Task.Delay(5000, stoppingToken);
            } while (!stoppingToken.IsCancellationRequested);
        }

        /// <summary>
        ///     Processes the background service.
        /// </summary>
        protected abstract Task Process();
    }
}