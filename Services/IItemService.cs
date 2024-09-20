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

    public class ItemService(ApplicationDbContext context, IBlobStorageService blobStorageService) : IItemService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IBlobStorageService _blobStorageService = blobStorageService;

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
                RemainingItem = item.RemainingItem,
                Description = item.Description,
                PictureUrl = item.PictureUrl

            }).ToList();
        }

        public async Task AddItemAsync(ItemViewModel model)
        {
            var id = Guid.NewGuid();
            var newItem = new Item
            {
                Id = id,
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.Category!.Id!.Value,
                RemainingItem = model.RemainingItem,
                Description = model.Description
            };

            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();

            var entity = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == default)
            {
                throw new Exception($"Cannot found the item {id}");
            }

            var url = await _blobStorageService.UploadFileAsync(model.Picture, $"{Guid.NewGuid()}");

            entity.PictureUrl = url;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                await _blobStorageService.DeleteBlobAsync(item!.PictureUrl);
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
                existingItem.CategoryId = model.Category!.Id!.Value;
                existingItem.RemainingItem = model.RemainingItem;
                existingItem.Description = model.Description;

                var url = await _blobStorageService.UploadFileAsync(model.Picture, $"{Guid.NewGuid()}");

                await _blobStorageService.DeleteBlobAsync(existingItem!.PictureUrl);

                existingItem.PictureUrl = url;

                await _context.SaveChangesAsync();
            }
        }
    }
}
