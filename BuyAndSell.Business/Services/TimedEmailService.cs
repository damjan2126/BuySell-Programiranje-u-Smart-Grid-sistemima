using BuySell.Business.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Services
{
    public class TimedEmailService : BackgroundService
    {
        public IServiceProvider Services { get; }
        private readonly ILogger<TimedEmailService> _logger;

        public TimedEmailService(IServiceProvider services, ILogger<TimedEmailService> logger)
        {
            Services = services;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting email service...");
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using var scope = Services.CreateScope();
            var scopedProcessingService =
                scope.ServiceProvider
                    .GetRequiredService<IScopedEmailService>();

            await scopedProcessingService.DoWork(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Emailer Service");
            await base.StopAsync(cancellationToken);
        }
    }
}
