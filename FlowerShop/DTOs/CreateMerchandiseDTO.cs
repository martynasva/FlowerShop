using FlowerShop.Models;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.DTOs
{
    public class CreateMerchandiseDTO 
    {
        [Required]
        public string Name { get; set; }

        [StringLength(maximumLength:255)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }


        public static Merchandise ToMerchandise(CreateMerchandiseDTO createMerchandiseDTO)
        {
            return new Merchandise
            {
                ID = Guid.NewGuid(),
                Name = createMerchandiseDTO.Name,
                Description = createMerchandiseDTO.Description,
                Price = createMerchandiseDTO.Price
            };
        }
    }
}
