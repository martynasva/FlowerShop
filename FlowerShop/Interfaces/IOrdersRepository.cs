using FlowerShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Interfaces
{
    public interface IOrdersRepository
    {
        public Task<IEnumerable<Order>> GetAll();

        public Task<Order?> GetById(Guid id);

        public Task<IEnumerable<Order>> GetBy(
            Guid? userID = null,
            string? status = null);

        public Task<Order> AddOrder(Order order);

        public Task<Order?> RemoveOrder(Guid orderID);

        public Task<Order?> UpdateOrder(Order updatedOrder);
    }
}
