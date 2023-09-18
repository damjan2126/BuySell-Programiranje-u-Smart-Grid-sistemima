using BuySell.Data.Enums;
using BuySell.Data.Repositories.Contracts;
using BuySell.Data.Resources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sgbj.Cron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Services
{
    public class OrderUpdateService : BackgroundService
    {
        private readonly IServiceProvider Services;

        public OrderUpdateService(IServiceProvider services)
        {
            Services = services;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var _timer = new CronTimer("* * * * *", TimeZoneInfo.Local);
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                using var scope = Services.CreateScope();
                var orderRepository =
                    scope.ServiceProvider
                        .GetRequiredService<IOrderRepository>();

                var orders = await orderRepository.GetAllAsync(new OrderQuery()
                {
                    MaxDeliveryTime = DateTime.UtcNow
                });

                foreach(var order in orders)
                {
                    order.Status = Data.Enums.OrderStatusEnum.Completed;
                    await orderRepository.UpdateAsync(order);
                }
            }
        }

    }
}
