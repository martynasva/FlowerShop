using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DataContext _dataContext;

        public OrdersRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Order>> GetAll() => await _dataContext.Orders.ToListAsync();

        public async Task<Order?> GetById(Guid id)
        {
            return await _dataContext.Orders.SingleOrDefaultAsync(i => i.ID == id);
        }

        public async Task<IEnumerable<Order>> GetBy(
            string? userName = null,
            string? status = null)
        {
            var query = _dataContext.Orders.AsQueryable();

            if (userName != null) query = query.Where(i => i.User.Name == userName);
            if (!string.IsNullOrEmpty(status)) query = query.Where(i => i.OrderStatus != null && i.OrderStatus.ToString().ToLower() == status.ToLower());

            return await query.ToListAsync();
        }

        public async Task<Order> AddOrder(Order order)
        {
            _dataContext.Orders.Add(order);
            await _dataContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> RemoveOrder(Guid orderId)
        {
            var order = await GetById(orderId);
            if (order == null) return null;
            _dataContext.Orders.Remove(order);
            await _dataContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateOrder(Order updatedOrder)
        {
            var order = await GetById(updatedOrder.ID);
            if (order == null) return null;
            _dataContext.Entry(order).CurrentValues.SetValues(updatedOrder);
            await _dataContext.SaveChangesAsync();
            return updatedOrder;
        }
    }
}
