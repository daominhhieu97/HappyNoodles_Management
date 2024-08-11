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
    Task UpdateCategoryAsync(CategoryViewModel model);
    Task DeleteCategory(Guid id);
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

    public async Task UpdateCategoryAsync(CategoryViewModel model)
    {
        var category = await _context.Categories.FindAsync(model.Id);

        if (category != null)
        {
            category.Name = model.Name;
            category.MenuId = model.Menu.Id;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCategory(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
