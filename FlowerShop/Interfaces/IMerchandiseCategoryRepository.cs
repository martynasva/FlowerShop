using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IMerchandiseCategoryRepository
    {
        public Task<IEnumerable<MerchandiseCategory>> GetAll();

        public Task<MerchandiseCategory?> GetById(Guid id);

        public Task<IEnumerable<MerchandiseCategory>> GetBy(
            string? name = null);

        public Task<MerchandiseCategory> AddMerchandiseCategory(MerchandiseCategory merchandiseCategory);

        public Task<MerchandiseCategory?> DeleteMerchandiseCateogry(Guid id);

        public Task<MerchandiseCategory?> UpdateMerchandiseCategory(MerchandiseCategory merchandiseCategory);
    }
}
