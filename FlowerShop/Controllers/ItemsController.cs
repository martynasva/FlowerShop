using FlowerShop.DTOs;
using FlowerShop.Interfaces;
using FlowerShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    public class ItemsController : BaseApiController
    {
        private readonly IItemsRepository _itemsRepository;
        private readonly IOrdersRepository _ordersRepository;

        public ItemsController(IItemsRepository itemsRepository, IOrdersRepository ordersRepository)
        {
            _itemsRepository = itemsRepository;
            _ordersRepository = ordersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems(
            [FromQuery] Guid? merchandiceId = null,
            [FromQuery] string? countryOfOrigin = null,
            [FromQuery] DateTime? manufacturedFrom = null,
            [FromQuery] DateTime? manufacturedTo = null)
        {
            var items = (await _itemsRepository.GetBy(merchandiceId, countryOfOrigin, manufacturedFrom, manufacturedTo)).Select(i => ItemDTO.FromItem(i));
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemById(Guid id)
        {
            var item = await _itemsRepository.GetById(id);
            return item == null ? NotFound() : Ok(ItemDTO.FromItem(item)); 
        }
        
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> AddItem([FromBody] CreateItemDTO createItemDTO)
        {
            var item = CreateItemDTO.ToItem(createItemDTO);
            var createdItem = await _itemsRepository.AddItem(item);
            return Ok(ItemDTO.FromItem(createdItem));
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDTO>> UpdateItem(Guid itemId ,[FromBody] ItemDTO updatedItem)
        {
            if (itemId != updatedItem.ID) return BadRequest();
            var item = await _itemsRepository.UpdateItem(ItemDTO.ToItem(updatedItem));
            return item == null ? NotFound() : Ok(ItemDTO.FromItem(item));
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> DeleteItem(Guid id)
        {
            var item = await _itemsRepository.RemoveItem(id);
            return item == null ? NotFound() : Ok(ItemDTO.FromItem(item));
        }

        [HttpPut("addItem/{itemId}/{orderId}")]
        public async Task<ActionResult<ItemDTO>> AddItemToCart(Guid itemId, Guid orderId)
        {
            var order = await _ordersRepository.GetById(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var item = await _itemsRepository.GetById(itemId);
            if (item == null)
            {
                return NotFound();
            }

            var updatedItem = await _itemsRepository.AddItemToOrder(item, order);
            return updatedItem == null ? NotFound() : Ok(ItemDTO.FromItem(updatedItem));
        }
    }
}
