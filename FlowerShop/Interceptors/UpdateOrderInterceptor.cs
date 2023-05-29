using FlowerShop.Interfaces;
using FlowerShop.Models;
using FlowerShop.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FlowerShop.Interceptors
{
    public class UpdateOrderInterceptor : SaveChangesInterceptor
    {
        private readonly IServiceProvider _serviceProvider;

        public UpdateOrderInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;

            if (dbContext == null) 
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var entries = dbContext.ChangeTracker.Entries<Order>();
            var separateScope = _serviceProvider.CreateScope();
            var separateServiceProvider = separateScope.ServiceProvider;
            var orderLogsRepository = separateServiceProvider.GetRequiredService<IOrderLogsRepository>();

            OrderLog orderLog = new OrderLog();
            foreach (EntityEntry<Order> entityEntry in entries)
            {
                switch(entityEntry.State)
                {
                    case EntityState.Deleted:
                        orderLog.Timestamp = DateTime.UtcNow;
                        orderLog.LogText = "Order Deleted";

                        orderLog.OrderID = entityEntry.Entity.ID;

                        orderLogsRepository.AddOrderLog(orderLog);
                        break;
                    case EntityState.Modified:
                        orderLog.Timestamp = DateTime.UtcNow;
                        orderLog.LogText = GetLogText(entityEntry);

                        orderLog.OrderID = entityEntry.Entity.ID;

                        orderLogsRepository.AddOrderLog(orderLog);
                        break;
                    default:
                        break;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private string GetLogText(EntityEntry entityEntry)
        {
            string logText = "Order Updated. Changes: ";

            foreach (var propertyEntry in entityEntry.Properties)
            {
                if (propertyEntry.IsModified)
                {
                    string propertyName = propertyEntry.Metadata.Name;
                    object originalValue = propertyEntry.OriginalValue;
                    object currentValue = propertyEntry.CurrentValue;

                    string changeText = $"{propertyName}: From '{originalValue}' to '{currentValue}'";
                    logText += changeText + ", ";
                }
            }

            logText = logText.TrimEnd(',', ' ');

            return logText;
        }
    }
}
