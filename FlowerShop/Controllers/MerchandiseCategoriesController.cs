﻿using FlowerShop.DTOs;
using FlowerShop.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    public class MerchandiseCategoriesController : BaseApiController
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
            return Ok(merchandiseCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MerchandiseCategoryDTO>> GetMerchandiseCategoryById(Guid id)
        {
            var merchandiseCategory = await _merchandiseCategoryRepository.GetById(id);

            return merchandiseCategory == null ? NotFound() : Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(merchandiseCategory));
        }

        [HttpPost]
        public async Task<ActionResult<MerchandiseCategoryDTO>> AddMerchandiseCategory([FromBody] CreateMerchandiseCategoryDTO createMerchandiseCategory)
        {
            var merchandiseCategory = CreateMerchandiseCategoryDTO.ToMerchandiseCategory(createMerchandiseCategory);
            var created = await _merchandiseCategoryRepository.AddMerchandiseCategory(merchandiseCategory);
            return Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(created));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<MerchandiseCategoryDTO>> UpdateMerchandiseCategory(Guid id, [FromBody] MerchandiseCategoryDTO updatedMerchandiseCategory)
        {
            if (id != updatedMerchandiseCategory.ID)
                return BadRequest();

            var merchandiseCategory = await _merchandiseCategoryRepository.UpdateMerchandiseCategory(MerchandiseCategoryDTO.ToMerchandiseCategory(updatedMerchandiseCategory));
            return merchandiseCategory == null ? NotFound() : Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(merchandiseCategory));
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<MerchandiseCategoryDTO>>> DeleteMerchandiseCategory(Guid id)
        {
            var merchandiseCategory = await _merchandiseCategoryRepository.DeleteMerchandiseCateogry(id);

             return merchandiseCategory == null ? NotFound() : Ok(MerchandiseCategoryDTO.FromMerchandiseCategory(merchandiseCategory));
        }

        
        [HttpGet("{id}/children")]
        public async Task<ActionResult<IEnumerable<MerchandiseCategoryDTO>>> GetMerchandiseCategoryChildren(Guid id)
        {
            var childCategories = await _merchandiseCategoryRepository.GetChildCategories(id);

            return childCategories == null ? NotFound() : Ok(childCategories.Select(async c => MerchandiseCategoryDTO.FromMerchandiseCategory(c)));
        }
        
    }
}