using FlowerShop.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FlowerShop.DTOs;
using FlowerShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class MerchandisesController : ControllerBase
    {
        private readonly IMerchandiseRepository _merchandiseRepository;

        public MerchandisesController(IMerchandiseRepository merchandiseRepository)
        {
            _merchandiseRepository = merchandiseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MerchandiseDTO>>> GetMerchandises(
            [FromQuery] string? merchandiseName = null,
            [FromQuery] decimal? price = null)
        {
            var merchandises = (await _merchandiseRepository.GetBy(merchandiseName, price)).Select(m => MerchandiseDTO.FromMerchandise(m));
            return Ok(merchandises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MerchandiseDTO>> GetMerchandiseById(Guid id)
        {
            var merchandise = await _merchandiseRepository.GetById(id);
            return merchandise == null ? NotFound() : Ok(MerchandiseDTO.FromMerchandise(merchandise));
        }

        [HttpPost]
        public async Task<ActionResult<MerchandiseDTO>> AddMerchandise([FromBody] CreateMerchandiseDTO createMerchandiseDTO)
        {
            var merchandise = CreateMerchandiseDTO.ToMerchandise(createMerchandiseDTO);
            var created = await _merchandiseRepository.AddMerchandise(merchandise);
            return Ok(MerchandiseDTO.FromMerchandise(created));
        }

        [HttpPut("{id}/{version}")]
        public async Task<ActionResult<MerchandiseDTO>> UpdateMerchandise([FromRoute] Guid id, [FromRoute] uint version, [FromBody] MerchandiseDTO updateMerchandise)
        {
            if (id != updateMerchandise.Id || version != updateMerchandise.Version)
                return BadRequest();
            try
            {
                var merchandise = await _merchandiseRepository.UpdateMerchandise(MerchandiseDTO.ToMerchandise(updateMerchandise));
                return merchandise == null ? NotFound() : Ok(MerchandiseDTO.FromMerchandise(merchandise));
            }
            catch(DbUpdateConcurrencyException)
            {
                var databaseEntry = await _merchandiseRepository.GetById(id);
                if(databaseEntry == null)
                {
                    return Conflict("Unable to save changes. The Merchandise was deleted by another user.");
                }
                return Conflict(MerchandiseDTO.FromMerchandise(databaseEntry));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<MerchandiseDTO>>> DeleteMerchandise(Guid id)
        {
            var merchandise = await _merchandiseRepository.RemoveMerchandise(id);
            return merchandise == null ? NotFound() : Ok(MerchandiseDTO.FromMerchandise(merchandise));
        }
    }
}
