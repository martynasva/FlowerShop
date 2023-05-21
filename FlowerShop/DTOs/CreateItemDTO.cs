using FlowerShop.Models;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.DTOs
{
    public class CreateItemDTO
    {
        [Required]
        public Guid MerchandiceID { get; set; }

        [StringLength(maximumLength:60)]
        public string? CountryOfOrigin { get; set; }

        public DateTime? DateOfManufacture { get; set; }

        public static Item ToItem(CreateItemDTO createItemDTO)
        {
            return new Item
            {
                ID = Guid.NewGuid(),
                MerchandiceID = createItemDTO.MerchandiceID,
                CountryOfOrigin = createItemDTO.CountryOfOrigin,
                DateOfManufacture = createItemDTO.DateOfManufacture.HasValue ? createItemDTO.DateOfManufacture.Value.ToUniversalTime() : null,
            };
        }
    }
}
