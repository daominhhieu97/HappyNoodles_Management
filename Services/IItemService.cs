//using HappyNoodles_ManagementApp.Models;
//using HappyNoodles_ManagementWebApp.Data;
//using Microsoft.EntityFrameworkCore;

//public interface IItemService
//{
//    Task<List<Item>> GetFoodItems(int categoryId);
//    Task<Item> GetFoodItem(int id);
//    Task AddFoodItem(Item foodItem);
//    Task UpdateFoodItem(Item foodItem);
//    Task DeleteFoodItem(int id);
//}


//public class FoodItemService : IItemService
//{
//    private readonly ApplicationDbContext _context;

//    public FoodItemService(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<List<Item>> GetFoodItems(int categoryId)
//    {
//        return await _context.FoodItems.Where(f => f.CategoryId == categoryId).ToListAsync();
//    }

//    public async Task<Item> GetFoodItem(int id) => await _context.FoodItems.FirstOrDefaultAsync(f => f.Id == id);

//    public async Task AddFoodItem(Item foodItem)
//    {
//        _context.FoodItems.Add(foodItem);
//        await _context.SaveChangesAsync();
//    }

//    public async Task UpdateFoodItem(Item foodItem)
//    {
//        _context.Entry(foodItem).State = EntityState.Modified;
//        await _context.SaveChangesAsync();
//    }

//    public async Task DeleteFoodItem(int id)
//    {
//        var foodItem = await _context.FoodItems.FindAsync(id);
//        if (foodItem != null)
//        {
//            _context.FoodItems.Remove(foodItem);
//            await _context.SaveChangesAsync();
//        }
//    }
//}
