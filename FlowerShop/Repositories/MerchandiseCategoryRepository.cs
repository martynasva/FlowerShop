using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Repositories
{
    public class MerchandiseCategoryRepository : IMerchandiseCategoryRepository
    {
        private readonly DataContext _dataContext;

        public MerchandiseCategoryRepository(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task<MerchandiseCategory> AddMerchandiseCategory(MerchandiseCategory merchandiseCategory)
        {
            _dataContext.MerchandiseCategories.Add(merchandiseCategory);
            await _dataContext.SaveChangesAsync();
            return merchandiseCategory;
        }

        public async Task<MerchandiseCategory?> DeleteMerchandiseCateogry(Guid id)
        {
            var merchandise = await GetById(id);
            if (merchandise == null)
                return null;
            _dataContext.MerchandiseCategories.Remove(merchandise);
            await _dataContext.SaveChangesAsync();
            return merchandise;
        }

        public async Task<IEnumerable<MerchandiseCategory>> GetAll() => await _dataContext.MerchandiseCategories.ToListAsync();

        public async Task<IEnumerable<MerchandiseCategory>> GetBy(string? name = null, Guid? parentCategory = null)
        {
            var query = _dataContext.MerchandiseCategories.AsQueryable();

            if(!string.IsNullOrEmpty(name)) 
                query = query.Where(m => m.Name != null && m.Name.ToLower() == name.ToLower());
            if (parentCategory != null)
                query = query.Where(m => m.ParentCategoryID == parentCategory);
            return await query.ToListAsync();
        }

        public async Task<MerchandiseCategory?> GetById(Guid id)
        {
            return await _dataContext.MerchandiseCategories.SingleOrDefaultAsync(m => m.ID == id);
        }

        public async Task<MerchandiseCategory?> UpdateMerchandiseCategory(MerchandiseCategory merchandiseCategory)
        {
            var merchandise = await GetById(merchandiseCategory.ID);
            if(merchandise == null) 
                return null;
            _dataContext.Entry(merchandise).CurrentValues.SetValues(merchandiseCategory);
            await _dataContext.SaveChangesAsync();
            return merchandiseCategory;
        }
    }
}
