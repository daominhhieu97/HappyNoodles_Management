using HappyNoodles_ManagementApp.Models;
using HappyNoodles_ManagementApp.ViewModels.Menu;
using HappyNoodles_ManagementWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HappyNoodles_ManagementApp.Services
{
    public interface IMenuService
    {
        Task<List<Menu>> GetMenus();
        Task<Menu> GetMenu(Guid id);
        Task AddMenu(Menu menu);
        Task UpdateMenu(MenuViewModel menu);
        Task DeleteMenu(Guid id);
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

        public async Task<Menu> GetMenu(Guid id)
        {
            return await _context.Menus.Include(m => m.Categories).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddMenu(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenu(MenuViewModel menu)
        {
            var result = _context.Menus.FirstOrDefault(m => m.Id == menu.Id);
            if (result == null)
            {
                return;
            }

            result.Name = menu.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenu(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }
    }
}