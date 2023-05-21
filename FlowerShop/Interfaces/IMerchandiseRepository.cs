using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IMerchandiseRepository
    {
        public Task<Merchandise> AddMerchandise(Merchandise merchandise);

        public Task<IEnumerable<Merchandise>> GetAll();

        public Task<Merchandise?> GetById(Guid id);

        public Task<IEnumerable<Merchandise>> GetBy(
            string? merchandiseName = null,
            decimal? price = null);

        public Task<Merchandise> UpdateMerchandise(Merchandise updatedMerchandise);

        public Task<Merchandise> RemoveMerchandise(Guid id);




    }
}
