﻿
using HappyNoodles_ManagementApp.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace HappyNoodles_ManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<List<CategoryViewModel>> GetCategories()
        {
           var categories = await _categoryService.GetCategories();
           var categoryViewModel = categories.Select(x => new CategoryViewModel
           {
               Id = x.Id,
               Name = x.Name, 
               Menu = new CategoryMenuViewModel
               {
                   Id = x.MenuId,
                   Name = x.Menu.Name,
               }
           }).ToList();

            return categoryViewModel;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel model)
        {
            await _categoryService.AddCategoryAsync(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategory(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryViewModel model)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch");

            await _categoryService.UpdateCategoryAsync(model);

            return Ok();
        }
    }
}
