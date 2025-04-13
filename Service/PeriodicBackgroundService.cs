namespace CancelationTokenWebApp.Service
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class PeriodicBackgroundService : BackgroundService
    {
        private readonly ILogger<PeriodicBackgroundService> _logger;

        public PeriodicBackgroundService(ILogger<PeriodicBackgroundService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Periodic Background Service is starting.");

            // ایجاد یک PeriodicTimer با فاصله زمانی 5 ثانیه
            using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    // انجام کار پشت صحنه
                    DoBackgroundWork();

                    _logger.LogInformation("Background work executed at: {Time}", DateTimeOffset.Now);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Periodic Background Service is stopping.");
            }
        }

        private void DoBackgroundWork()
        {
            // منطق کار پشت صحنه
            // مثلاً انجام یک عملیات I/O یا محاسباتی
            Console.WriteLine("Performing background task...");
        }
    }
}
