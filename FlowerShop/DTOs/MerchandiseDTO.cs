using FlowerShop.Models;

namespace FlowerShop.DTOs
{
    public class MerchandiseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public static MerchandiseDTO FromMerchandise(Merchandise merchandise)
        {
            return new MerchandiseDTO
            {
                Id = merchandise.ID,
                Name = merchandise.Name,
                Description = merchandise.Description,
                Price = merchandise.Price
            };
        }

        public static Merchandise ToMerchandise(MerchandiseDTO merchandiseDTO)
        {
            return new Merchandise
            {
                ID = merchandiseDTO.Id,
                Name = merchandiseDTO.Name,
                Description = merchandiseDTO.Description,
                Price = merchandiseDTO.Price
            };
        }

    }
}
