using FlowerShop.DTOs;
using FlowerShop.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
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

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> AddItem([FromBody] CreateItemDTO createItemDTO)
        {
            var item = CreateItemDTO.ToItem(createItemDTO);
            var createdItem = await _itemsRepository.AddItem(item);
            return Ok(ItemDTO.FromItem(createdItem));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDTO>> UpdateItem(Guid itemId ,[FromBody] ItemDTO updatedItem)
        {
            if (itemId != updatedItem.ID) return BadRequest();
            var item = await _itemsRepository.UpdateItem(ItemDTO.ToItem(updatedItem));
            return item == null ? NotFound() : Ok(ItemDTO.FromItem(item));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> DeleteItem(Guid id)
        {
            var item = await _itemsRepository.RemoveItem(id);
            return item == null ? NotFound() : Ok(ItemDTO.FromItem(item));
        }
    }
}
