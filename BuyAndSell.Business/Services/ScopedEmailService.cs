using BuySell.Business.Services.Contracts;
using BuySell.Business.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Services
{
    public class ScopedEmailService : IScopedEmailService
    {
        private readonly IEmailService _emailService;
        private readonly EmailConfigurationSettings _emailConfigurationSettings;
        private readonly ILogger<ScopedEmailService> _logger;

        public ScopedEmailService(IEmailService emailService,
                                  IOptions<EmailConfigurationSettings> emailConfigurationSettings,
                                  ILogger<ScopedEmailService> logger)
        {
            _emailService = emailService;
            _emailConfigurationSettings = emailConfigurationSettings.Value;
            _logger = logger;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _emailService.SendEmailAsync(_emailConfigurationSettings.Host);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception executing tasks.");
                }
                await Task.Delay(_emailConfigurationSettings.EmailJobInterval, stoppingToken);
            }
        }
    }
}
