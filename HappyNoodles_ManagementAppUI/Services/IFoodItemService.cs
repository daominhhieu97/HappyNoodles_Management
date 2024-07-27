using HappyNoodles_ManagementApp.Models;
using HappyNoodles_ManagementWebApp.Data;
using Microsoft.EntityFrameworkCore;

public interface IFoodItemService
{
    Task<List<FoodItem>> GetFoodItems(int categoryId);
    Task<FoodItem> GetFoodItem(int id);
    Task AddFoodItem(FoodItem foodItem);
    Task UpdateFoodItem(FoodItem foodItem);
    Task DeleteFoodItem(int id);
}


public class FoodItemService : IFoodItemService
{
    private readonly ApplicationDbContext _context;

    public FoodItemService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<FoodItem>> GetFoodItems(int categoryId)
    {
        return await _context.FoodItems.Where(f => f.CategoryId == categoryId).ToListAsync();
    }

    public async Task<FoodItem> GetFoodItem(int id) => await _context.FoodItems.FirstOrDefaultAsync(f => f.Id == id);

    public async Task AddFoodItem(FoodItem foodItem)
    {
        _context.FoodItems.Add(foodItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFoodItem(FoodItem foodItem)
    {
        _context.Entry(foodItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFoodItem(int id)
    {
        var foodItem = await _context.FoodItems.FindAsync(id);
        if (foodItem != null)
        {
            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();
        }
    }
}
