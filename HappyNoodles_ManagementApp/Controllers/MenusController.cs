

using HappyNoodles_ManagementApp.Models;
using HappyNoodles_ManagementApp.Services;
using HappyNoodles_ManagementApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HappyNoodles_ManagementWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // GET: api/menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuViewModel>>> GetMenus()
        {
            var menus = await _menuService.GetMenus();

            return Ok(menus.Select(x => new MenuViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }));
        }

        // GET: api/menus/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Menu>> GetMenu(int id)
        //{
        //    var menu = await _menuService.GetMenu(id);

        //    if (menu == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(menu);
        //}

        // POST: api/menus
        [HttpPost]
        public async Task<ActionResult> PostMenu(AddMenuViewModel menu)
        {
           await _menuService.AddMenu(new Menu()
           {
               Name = menu.Name
           });
           
           return Ok();
        }

        //// PUT: api/menus/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMenu(int id, Menu menu)
        //{
        //    if (id != menu.Id)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        await _menuService.UpdateMenu(menu);
        //    }
        //    catch (Exception)
        //    {
        //        if (await _menuService.GetMenu(id) == null)
        //        {
        //            return NotFound();
        //        }
        //        throw;
        //    }

        //    return NoContent();
        //}

        // DELETE: api/menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
           await _menuService.DeleteMenu(id);
           return NoContent();
        }
    }
}
