using FlowerShop.Models;
using FlowerShop.Repositories;

namespace FlowerShop.DTOs
{
    public class MerchandiseCategoryDTO
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public Guid? ParentCategoryID { get; set; }


        public List<MerchandiseCategoryDTO> Categories { get; set; } 

        public static MerchandiseCategoryDTO FromMerchandiseCategory(MerchandiseCategory merchandiseCategory, List<MerchandiseCategory> allCategories)
        {
            return new MerchandiseCategoryDTO
            {
                ID = merchandiseCategory.ID,
                Name = merchandiseCategory.Name,
                ParentCategoryID = merchandiseCategory.ParentCategoryID,
                Categories = GetChildCategories(merchandiseCategory.ID, allCategories)
            };
        }

        public static MerchandiseCategory ToMerchandiseCategory(MerchandiseCategoryDTO merchandiseCategoryDTO)
        {
            return new MerchandiseCategory
            {
                ID = merchandiseCategoryDTO.ID,
                Name = merchandiseCategoryDTO.Name,
                ParentCategoryID = merchandiseCategoryDTO.ParentCategoryID
            };
        }

        public static List<MerchandiseCategoryDTO> GetChildCategories(Guid id, List<MerchandiseCategory> allCategories)
        {
            List<MerchandiseCategoryDTO> childCategories = new List<MerchandiseCategoryDTO>();

            foreach (var child in allCategories)
            {
                if(child.ParentCategoryID == id)
                {
                    var childCategory = new MerchandiseCategoryDTO
                    {
                        ID = child.ID,
                        Name = child.Name,
                        ParentCategoryID = child.ParentCategoryID,
                        Categories = GetChildCategories(child.ID, allCategories)
                    };
                    childCategories.Add(childCategory);
                }
            }
            return childCategories;
        }
    }
}
