using HappyNoodles_ManagementWebApp.Models;
using HappyNoodles_ManagementWebApp.Data;
using Microsoft.EntityFrameworkCore;

public interface IMenuService
{
    Task<List<Menu>> GetMenus();
    Task<Menu> GetMenu(int id);
    Task AddMenu(Menu menu);
    Task UpdateMenu(Menu menu);
    Task DeleteMenu(int id);
}

public class MenuService : IMenuService
{
    private readonly ApplicationDbContext _context;

    public MenuService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Menu>> GetMenus()
    {
        return await _context.Menus.Include(m => m.Categories).ToListAsync();
    }

    public async Task<Menu> GetMenu(int id)
    {
        return await _context.Menus.Include(m => m.Categories).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddMenu(Menu menu)
    {
        _context.Menus.Add(menu);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMenu(Menu menu)
    {
        _context.Entry(menu).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMenu(int id)
    {
        var menu = await _context.Menus.FindAsync(id);
        if (menu != null)
        {
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }
    }
}

