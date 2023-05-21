using FlowerShop.Models;

namespace FlowerShop.DTOs
{
    public class ItemDTO
    {
        public Guid ID { get; set; }

        public Guid MerchandiceID { get; set; }

        public string? CountryOfOrigin { get; set; }

        public DateTime? DateOfManufacture { get; set; }

        public static ItemDTO FromItem(Item item)
        {
            return new ItemDTO
            {
                ID = item.ID,
                MerchandiceID = item.MerchandiceID,
                CountryOfOrigin = item.CountryOfOrigin,
                DateOfManufacture = item.DateOfManufacture,
            };
        }

        public static Item ToItem(ItemDTO itemDTO)
        {
            return new Item
            {
                ID = itemDTO.ID,
                MerchandiceID = itemDTO.MerchandiceID,
                CountryOfOrigin = itemDTO.CountryOfOrigin,
                DateOfManufacture = itemDTO.DateOfManufacture,
            };
        }
    }
}
