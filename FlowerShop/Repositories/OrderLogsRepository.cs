using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Repositories
{
    public class OrderLogsRepository : IOrderLogsRepository
    {
        private readonly DataContext _dataContext;

        public OrderLogsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<OrderLog>> GetAll() => await _dataContext.OrderLogs.ToListAsync();

        public async Task<OrderLog> AddOrderLog(OrderLog orderLog)
        {
            _dataContext.OrderLogs.Add(orderLog);
            await _dataContext.SaveChangesAsync();
            return orderLog;
        }

        public async Task<OrderLog> GetById(Guid id)
        {
            return await _dataContext.OrderLogs.SingleOrDefaultAsync(i => i.ID == id);
        }

        public async Task<IEnumerable<OrderLog>> GetByOrder(Guid orderId)
        {
            var query = _dataContext.OrderLogs.AsQueryable();

            if (orderId != null) query = query.Where(i => i.OrderID == orderId);

            return await query.ToListAsync();
        }
    }
}
