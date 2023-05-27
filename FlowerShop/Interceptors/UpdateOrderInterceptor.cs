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
        private bool isInterceptionDisabled = false;
        private readonly IServiceProvider _serviceProvider;

        public UpdateOrderInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (isInterceptionDisabled)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            DbContext? dbContext = eventData.Context;

            if (dbContext == null) 
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var entries = dbContext.ChangeTracker.Entries<Order>();

            foreach(EntityEntry<Order> entityEntry in entries)
            {
                switch(entityEntry.State)
                {
                    case EntityState.Added:
                        Console.WriteLine("AddedOrder");
                        break;
                    case EntityState.Deleted:
                        Console.WriteLine("DeletedOrder");
                        break;
                    case EntityState.Modified:
                        var separateScope = _serviceProvider.CreateScope();
                        var separateServiceProvider = separateScope.ServiceProvider;
                        var orderLogsRepository = separateServiceProvider.GetRequiredService<IOrderLogsRepository>();

                        string newValues = entityEntry.CurrentValues.ToString();
                        string originalValues = entityEntry.OriginalValues.ToString();
                        OrderLog orderLog = new OrderLog();

                        orderLog.Timestamp = DateTime.UtcNow;
                        orderLog.LogText = GetLogText(entityEntry);

                        Guid orderId = entityEntry.Entity.ID;
                        orderLog.OrderID = orderId;
                        Console.WriteLine(orderLog.OrderID);


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

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            if (isInterceptionDisabled)
            {
                return result;
            }

            return base.SavedChanges(eventData, result);
        }
    }
}
