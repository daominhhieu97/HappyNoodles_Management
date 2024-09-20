using HappyNoodles_ManagementApp.Services;
using HappyNoodles_ManagementApp.ViewModels.Category;
using HappyNoodles_ManagementApp.ViewModels.Item;
using Microsoft.AspNetCore.Mvc;

namespace HappyNoodles_ManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<List<ItemViewModel>> GetItems()
        {
            var items = await _itemService.GetItemsAsync();
            var itemViewModels = items.Select(item => new ItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Category = new CategoryViewModel
                {
                    Id = item.Category!.Id,
                    Name = item.Category!.Name
                },
                RemainingItem = item.RemainingItem,
                Description = item.Description,
                PictureUrl = item.PictureUrl
            }).ToList();

            return itemViewModels;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(ItemViewModel model)
        {
            await _itemService.AddItemAsync(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            await _itemService.DeleteItemAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, ItemViewModel model)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch");

            await _itemService.UpdateItemAsync(model);
            return Ok();
        }
    }
}
