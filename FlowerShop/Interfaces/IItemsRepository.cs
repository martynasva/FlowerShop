using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IItemsRepository
    {
        public Task<IEnumerable<Item>> GetAll();

        public Task<Item?> GetById(Guid id);

        public Task<IEnumerable<Item>> GetBy(
            Guid? merchandiceId = null,
            string? countryOfOrigin = null,
            DateTime? manufacturedFrom = null,
            DateTime? manufacturedTo = null);

        public Task<Item> AddItem(Item item);

        public Task<Item?> RemoveItem(Guid itemId);

        public Task<Item?> UpdateItem(Item updatedItem);

        public Task<Item?> AddItemToOrder(Item item, Order orderToUpdate);

    }
}
