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
            List<MerchandiseCategory> allCategories = (List<MerchandiseCategory>)await _merchandiseCategoryRepository.GetAll();
            var merchandiseCategories = (await _merchandiseCategoryRepository.GetBy(name, parentCategory)).Select(m => MerchandiseCategoryDTO.FromMerchandiseCategory(m, allCategories));
            return Ok(merchandiseCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MerchandiseCategoryDTO>> GetMerchandiseCategoryById(Guid id)
        {
            var merchandiseCategory = await _merchandiseCategoryRepository.GetById(id);
            List<MerchandiseCategory> allCategories = (List<MerchandiseCategory>)await _merchandiseCategoryRepository.GetAll();

            return merchandiseCategory == null ? NotFound() : Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(merchandiseCategory, allCategories));
        }

        [HttpPost]
        public async Task<ActionResult<MerchandiseCategoryDTO>> AddMerchandiseCategory([FromBody] CreateMerchandiseCategoryDTO createMerchandiseCategory)
        {
            var merchandiseCategory = CreateMerchandiseCategoryDTO.ToMerchandiseCategory(createMerchandiseCategory);
            List<MerchandiseCategory> allCategories = (List<MerchandiseCategory>)await _merchandiseCategoryRepository.GetAll();
            var created = await _merchandiseCategoryRepository.AddMerchandiseCategory(merchandiseCategory);
            return Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(created, allCategories));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MerchandiseCategoryDTO>> UpdateMerchandiseCategory(Guid id, [FromBody] MerchandiseCategoryDTO updatedMerchandiseCategory)
        {
            if (id != updatedMerchandiseCategory.ID)
                return BadRequest();

            List<MerchandiseCategory> allCategories = (List<MerchandiseCategory>)await _merchandiseCategoryRepository.GetAll();
            var merchandiseCategory = await _merchandiseCategoryRepository.UpdateMerchandiseCategory(MerchandiseCategoryDTO.ToMerchandiseCategory(updatedMerchandiseCategory));
            return merchandiseCategory == null ? NotFound() : Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(merchandiseCategory, allCategories));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<MerchandiseCategoryDTO>>> DeleteMerchandiseCategory(Guid id)
        {
            var merchandiseCategory = await _merchandiseCategoryRepository.DeleteMerchandiseCateogry(id);
            List<MerchandiseCategory> allCategories = (List<MerchandiseCategory>)await _merchandiseCategoryRepository.GetAll();


             return merchandiseCategory == null ? NotFound() : Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(merchandiseCategory, allCategories));
        }
        
    }
}