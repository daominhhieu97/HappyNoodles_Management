using HappyNoodles_ManagementApp.Models;
using HappyNoodles_ManagementApp.ViewModels.Category;
using HappyNoodles_ManagementWebApp.Data;
using Microsoft.EntityFrameworkCore;

public interface ICategoryService
{
    Task<List<Category>> GetCategories();
    Task AddCategoryAsync(CategoryViewModel model);
    //Task<Category> GetCategory(int id);
    //Task AddCategory(Category category);
    //Task UpdateCategory(Category category);
    //Task DeleteCategory(int id);
}

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetCategories()
    {
        return await _context.Categories
            .Include(x => x.Menu)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task AddCategoryAsync(CategoryViewModel model)
    {
        var category = new Category
        {
            Name = model.Name,
            MenuId = model.Menu.Id
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    //public async Task<Category> GetCategory(int id)
    //{
    //    return await _context.Categories.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);
    //}

    //public async Task AddCategory(Category category)
    //{
    //    _context.Categories.Add(category);
    //    await _context.SaveChangesAsync();
    //}

    //public async Task UpdateCategory(Category category)
    //{
    //    _context.Entry(category).State = EntityState.Modified;
    //    await _context.SaveChangesAsync();
    //}

    //public async Task DeleteCategory(int id)
    //{
    //    var category = await _context.Categories.FindAsync(id);
    //    if (category != null)
    //    {
    //        _context.Categories.Remove(category);
    //        await _context.SaveChangesAsync();
    //    }
    //}
}
