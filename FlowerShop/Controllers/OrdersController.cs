using FlowerShop.DTOs;
using FlowerShop.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders(
            [FromQuery] string? userName = null,
            [FromQuery] string? status = null)
        {
            var orders = (await _ordersRepository.GetBy(userName, status)).Select(i => OrderDTO.FromOrder(i));
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(Guid id)
        {
            var order = await _ordersRepository.GetById(id);
            return order == null ? NotFound() : Ok(OrderDTO.FromOrder(order));
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> AddOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            var order = CreateOrderDTO.ToOrder(createOrderDTO);
            var createdOrder = await _ordersRepository.AddOrder(order);
            return Ok(OrderDTO.FromOrder(createdOrder));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDTO>> UpdateOrder(Guid orderId, [FromBody] OrderDTO updatedOrder)
        {
            if (orderId != updatedOrder.ID) return BadRequest();
            var order = await _ordersRepository.UpdateOrder(OrderDTO.ToOrder(updatedOrder));
            return order == null ? NotFound() : Ok(OrderDTO.FromOrder(order));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> DeleteOrder(Guid id)
        {
            var order = await _ordersRepository.RemoveOrder(id);
            return order == null ? NotFound() : Ok(OrderDTO.FromOrder(order));
        }
    }
}
