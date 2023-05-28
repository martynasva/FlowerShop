using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IOrderLogsRepository
    {
        public Task<IEnumerable<OrderLog>> GetAll();

        public Task<IEnumerable<OrderLog>> GetByOrder(Guid orderId);

        public Task<OrderLog?> GetById(Guid id);

        public Task<OrderLog> AddOrderLog(OrderLog orderLog);
    }
}
