using FlowerShop.DTOs;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchandiseCategoriesController : ControllerBase
    {
        private readonly IMerchandiseCategoryRepository _merchandiseCategoryRepository;

        public MerchandiseCategoriesController(IMerchandiseCategoryRepository merchandiseCategoryRepository)
        {
            _merchandiseCategoryRepository = merchandiseCategoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MerchandiseCategoryDTO>>> GetMerchandiseCategories(
            [FromQuery] string? name = null,
            [FromQuery] Guid? parentCategory = null)
        {
            var merchandiseCategories = (await _merchandiseCategoryRepository.GetBy(name, parentCategory)).Select(async m => MerchandiseCategoryDTO.FromMerchandiseCategory(m));
            // .Categories = (await _merchandiseCategoryRepository.GetChildCategories(m.ID)));

            return Ok(merchandiseCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MerchandiseCategoryDTO>> GetMerchandiseCategoryById(Guid id)
        {
            var merchandiseCategory = await _merchandiseCategoryRepository.GetById(id);
            List<MerchandiseCategory> allCategories = (List<MerchandiseCategory>)await _merchandiseCategoryRepository.GetAll();

            return merchandiseCategory == null ? NotFound() : Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(merchandiseCategory).Categories = (await _merchandiseCategoryRepository.GetChildCategories(id)));
        }

        [HttpPost]
        public async Task<ActionResult<MerchandiseCategoryDTO>> AddMerchandiseCategory([FromBody] CreateMerchandiseCategoryDTO createMerchandiseCategory)
        {
            var merchandiseCategory = CreateMerchandiseCategoryDTO.ToMerchandiseCategory(createMerchandiseCategory);
            var created = await _merchandiseCategoryRepository.AddMerchandiseCategory(merchandiseCategory);
            return Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(created));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MerchandiseCategoryDTO>> UpdateMerchandiseCategory(Guid id, [FromBody] MerchandiseCategoryDTO updatedMerchandiseCategory)
        {
            if (id != updatedMerchandiseCategory.ID)
                return BadRequest();

            var merchandiseCategory = await _merchandiseCategoryRepository.UpdateMerchandiseCategory(MerchandiseCategoryDTO.ToMerchandiseCategory(updatedMerchandiseCategory));
            return merchandiseCategory == null ? NotFound() : Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(merchandiseCategory));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<MerchandiseCategoryDTO>>> DeleteMerchandiseCategory(Guid id)
        {
            var merchandiseCategory = await _merchandiseCategoryRepository.DeleteMerchandiseCateogry(id);

             return merchandiseCategory == null ? NotFound() : Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(merchandiseCategory));
        }
        
    }
}