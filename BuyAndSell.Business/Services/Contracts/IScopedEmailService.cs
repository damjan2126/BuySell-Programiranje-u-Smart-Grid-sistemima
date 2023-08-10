using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Services.Contracts
{
    public interface IScopedEmailService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
