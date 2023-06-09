﻿using FlowerShop.Models;

namespace FlowerShop.DTOs
{
    public class MerchandiseCategoryDTO
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public Guid? ParentCategoryID { get; set; }

        public static MerchandiseCategoryDTO FromMerchandiseCategory(MerchandiseCategory merchandiseCategory)
        {
            return new MerchandiseCategoryDTO
            {
                ID = merchandiseCategory.ID,
                Name = merchandiseCategory.Name,
                ParentCategoryID = merchandiseCategory.ParentCategoryID
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

    }
}
