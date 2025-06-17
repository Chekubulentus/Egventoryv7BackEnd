using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Models.CategoryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egventoryv7BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            if (!categories.Any() || categories.Count() == 0)
                return NotFound("No Category Found");
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category is null)
                return NotFound("Category does not exist.");
            return Ok(category);
        }
        [HttpGet("Name")] 
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> SearchCategoryByNameAsync(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                return BadRequest("Invalid category name");
            var categories = await _categoryService.SearchCategoryByNameAsync(categoryName);
            if (!categories.Any() || categories.Count() == 0)
                return BadRequest($"{categoryName} does not exist.");
            return Ok(categories);
        }
        [HttpPost] 
        public async Task<ActionResult<bool>> CreateCategoryAsync(CreateCategoryRequest request)
        {
            if (!ModelState.IsValid || request is null)
                return BadRequest("Invalid entry.");
            var createCategory = await _categoryService.CreateCategoryAsync(request);
            if (!createCategory)
                return BadRequest("Category cannot be added.");
            return Ok(createCategory);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCategoryByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid entry.");
            var deleteCategory = await _categoryService.DeleteCategoryByIdAsync(id);
            if (!deleteCategory)
                return NotFound("Category does not exist.");
            return Ok(deleteCategory);
        }
    }
}
