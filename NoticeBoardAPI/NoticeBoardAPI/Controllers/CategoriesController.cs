using Microsoft.AspNetCore.Mvc;
using NoticeBoardAPI.Interfaces;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController: ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var responce = await _categoryService.GetAllCategoriesAsync();
            return Ok(responce);
        }

        [HttpGet("subcategories")]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var responce = await _categoryService.GetAllSubCategoriesAsync();
            return Ok(responce);
        }

        [HttpGet("{id}/subcategories")]
        public async Task<IActionResult> GetSubCategoriesByCategoryId(int id)
        {
            var responce = await _categoryService.GetSubCategoriesByCategoryIdAsync(id);
            if (!responce.Success)
            {
                return NotFound(responce);
            }
            return Ok(responce);
        }
    }
}
