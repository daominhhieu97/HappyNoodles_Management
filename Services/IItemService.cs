using HappyNoodles_ManagementApp.Models;
using HappyNoodles_ManagementApp.ViewModels.Category;
using HappyNoodles_ManagementApp.ViewModels.Item;
using HappyNoodles_ManagementWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HappyNoodles_ManagementApp.Services
{
    public interface IItemService
    {
        Task<List<ItemViewModel>> GetItemsAsync();
        Task AddItemAsync(ItemViewModel model);
        Task DeleteItemAsync(Guid id);
        Task UpdateItemAsync(ItemViewModel model);
    }

    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _context;

        public ItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItemViewModel>> GetItemsAsync()
        {
            var items = await _context.Items
                .Include(i => i.Category) // Assuming Category is a navigation property
                .ToListAsync();

            return items.Select(item => new ItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Category = new CategoryViewModel
                {
                    Id = item.Category.Id,
                    Name = item.Category.Name
                },
                RemainingItem = item.RemainingItem
            }).ToList();
        }

        public async Task AddItemAsync(ItemViewModel model)
        {
            var newItem = new Item
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.Category.Id!.Value,
                RemainingItem = model.RemainingItem
            };

            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateItemAsync(ItemViewModel model)
        {
            var existingItem = await _context.Items.FindAsync(model.Id);
            if (existingItem != null)
            {
                existingItem.Name = model.Name;
                existingItem.Price = model.Price;
                existingItem.CategoryId = model.Category.Id!.Value;
                existingItem.RemainingItem = model.RemainingItem;

                await _context.SaveChangesAsync();
            }
        }
    }
}
