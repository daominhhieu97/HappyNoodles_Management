
using HappyNoodles_ManagementApp.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace HappyNoodles_ManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CategoriesController : ControllerBase
    {
        public CategoriesController() { }

        [HttpGet]
        public List<CategoryViewModel> GetCategories()
        {
            return new List<CategoryViewModel>()
            {
                new CategoryViewModel()
                {
                     Id = Guid.Empty,
                Name = "",
                Menu = new CategoryMenuViewModel()
                {
                    Id = Guid.Empty,
                    Name = ""
                }
                }

            };
        }
    }
}
