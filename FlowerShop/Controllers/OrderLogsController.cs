using FlowerShop.DTOs;
using FlowerShop.Interfaces;
using FlowerShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    public class OrderLogsController : BaseApiController
    {
        private readonly IOrderLogsRepository _orderLogsRepository;

        public OrderLogsController(IOrderLogsRepository orderLogsRepository)
        {
            _orderLogsRepository = orderLogsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrderLogs()
        {
            var orderLogs = (await _orderLogsRepository.GetAll()).Select(i => OrderLogDTO.FromOrderLog(i));
            return Ok(orderLogs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderLogById(Guid id)
        {
            var orderLog = await _orderLogsRepository.GetById(id);
            return orderLog == null ? NotFound() : Ok(OrderLogDTO.FromOrderLog(orderLog));
        }

        [HttpGet("byOrder/{orderId}")]
        public async Task<ActionResult<OrderDTO>> GetOrderLogByOrderId(Guid orderId)
        {
            var orderLogs = (await _orderLogsRepository.GetByOrder(orderId)).Select(i => OrderLogDTO.FromOrderLog(i));
            return Ok(orderLogs);
        }
    }
}
