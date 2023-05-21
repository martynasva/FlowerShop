using FlowerShop.Models;
using FlowerShop.Repositories;

namespace FlowerShop.DTOs
{
    public class MerchandiseCategoryDTO
    {
        public Guid ID { get; set; }
        
        public Guid? ParentCategoryID { get; set; }

        public string Name { get; set; }

        public static MerchandiseCategoryDTO FromMerchandiseCategory(MerchandiseCategory merchandiseCategory)
        {
            return new MerchandiseCategoryDTO
            {
                ID = merchandiseCategory.ID,
                ParentCategoryID = merchandiseCategory.ParentCategoryID,
                Name = merchandiseCategory.Name,
            };
        }

        public static MerchandiseCategory ToMerchandiseCategory(MerchandiseCategoryDTO merchandiseCategoryDTO)
        {
            return new MerchandiseCategory
            {
                ID = merchandiseCategoryDTO.ID,
                ParentCategoryID = merchandiseCategoryDTO.ParentCategoryID,
                Name = merchandiseCategoryDTO.Name,
            };
        }
    }
}
