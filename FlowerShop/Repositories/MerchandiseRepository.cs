using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Repositories
{
    public class MerchandiseRepository : IMerchandiseRepository
    {
        private readonly DataContext _dataContext;

        public MerchandiseRepository(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task<Merchandise> AddMerchandise(Merchandise merchandise)
        {
            _dataContext.Merchandises.Add(merchandise);
            await _dataContext.SaveChangesAsync();
            return merchandise;
        }

        public async Task<IEnumerable<Merchandise>> GetAll() => await _dataContext.Merchandises.ToListAsync();

        public async Task<IEnumerable<Merchandise>> GetBy(
            string? merchandiseName = null, 
            decimal? price = null)
        {
            var query = _dataContext.Merchandises.AsQueryable();

            if(!string.IsNullOrEmpty(merchandiseName)) 
                query = query.Where(m => m.Name != null && m.Name.ToLower().Contains(merchandiseName.ToLower()));
            if(price != null) 
                query = query.Where(m => m.Price== price);

            return await query.ToListAsync();
        }

        public async Task<Merchandise?> GetById(Guid id)
        {
           return await _dataContext.Merchandises.SingleOrDefaultAsync(m => m.ID == id);
        }

        public async Task<Merchandise> RemoveMerchandise(Guid id)
        {
            var merchandise = await GetById(id);
            if (merchandise == null)
                return null;
            _dataContext.Merchandises.Remove(merchandise);
            await _dataContext.SaveChangesAsync();
            return merchandise;
        }

        public async Task<Merchandise> UpdateMerchandise(Merchandise updatedMerchandise)
        {
            var merchandise = await GetById(updatedMerchandise.ID);
            if (merchandise == null)
                return null;
            _dataContext.Entry(merchandise).CurrentValues.SetValues(updatedMerchandise);
            await _dataContext.SaveChangesAsync();
            return updatedMerchandise;
        }
    }
}
