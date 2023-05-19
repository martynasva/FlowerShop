using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly DataContext _dataContext;

        public ItemsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Item>> GetAll() => await _dataContext.Items.ToListAsync();

        public async Task<Item?> GetById(Guid id)
        {
            return await _dataContext.Items.SingleOrDefaultAsync(i => i.ID == id);
        }

        public async Task<IEnumerable<Item>> GetBy(
            Guid? merchandiceId = null, 
            string? countryOfOrigin = null, 
            DateTime? manufacturedFrom = null, 
            DateTime? manufacturedTo = null)
        {
            var query = _dataContext.Items.AsQueryable();

            if(merchandiceId != null) query = query.Where(i => i.MerchandiceID == merchandiceId);
            if(!string.IsNullOrEmpty(countryOfOrigin)) query = query.Where(i => i.CountryOfOrigin != null && i.CountryOfOrigin.ToLower() == countryOfOrigin.ToLower());
            if(manufacturedFrom.HasValue) query = query.Where(i => i.DateOfManufacture >= manufacturedFrom.Value.ToUniversalTime());
            if(manufacturedTo.HasValue) query = query.Where(i => i.DateOfManufacture <= manufacturedTo.Value.ToUniversalTime());

            return await query.ToListAsync();
        }
        
        public async Task<Item> AddItem(Item item)
        {
            _dataContext.Items.Add(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item?> RemoveItem(Guid itemId)
        {
            var item = await GetById(itemId);
            if(item == null) return null;
            _dataContext.Items.Remove(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item?> UpdateItem(Item updatedItem)
        {
            var item = await GetById(updatedItem.ID);
            if (item == null) return null;
            _dataContext.Entry(item).CurrentValues.SetValues(updatedItem);
            await _dataContext.SaveChangesAsync();
            return updatedItem;
        }
    }
}
