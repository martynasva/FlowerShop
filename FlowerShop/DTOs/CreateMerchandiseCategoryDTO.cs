using FlowerShop.Models;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.DTOs
{
    public class CreateMerchandiseCategoryDTO
    {
        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public static MerchandiseCategory ToMerchandiseCategory(CreateMerchandiseCategoryDTO createMerchandiseCategoryDTO)
        {
            return new MerchandiseCategory
            {
                ID = Guid.NewGuid(),
                ParentCategoryID = createMerchandiseCategoryDTO.ParentCategoryId,
                Name = createMerchandiseCategoryDTO.Name
            };
        }
    }
}
