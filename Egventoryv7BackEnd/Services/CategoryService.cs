using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.DataContext;
using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.CategoryModels;
using Microsoft.EntityFrameworkCore;

namespace Egventoryv7BackEnd.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepos;
        public CategoryService(ICategoryRepository repos, PoultryContext context)
        {
            _categoryRepos = repos;
        }
        public async Task<bool> CreateCategoryAsync(CreateCategoryRequest category)
        {
            if (category is null)
                return false;
            var newCategory = new Category
            {
                CategoryName = category.CategoryName
            };
            var createCategory = await _categoryRepos.CreateCategoryAsync(newCategory);
            if (!createCategory)
                return false;
            return createCategory;
        }
        public async Task<bool> DeleteCategoryByIdAsync(int id)
        {
            var deleteCategory = await _categoryRepos.DeleteCategoryByIdAsync(id);
            if (!deleteCategory)
                return false;
            return deleteCategory;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepos.GetAllCategoriesAsync();
            var modCategories = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                CategoryName = c.CategoryName
            });
            if (modCategories.Count() == 0)
                return new List<CategoryDTO>();
            return modCategories;
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepos.GetCategoryByIdAsync(id);
            if (category is null)
                return null;
            var modCategory = new CategoryDTO
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };
            return modCategory;
        }

        public async Task<IEnumerable<CategoryDTO>> SearchCategoryByNameAsync(string name)
        {
            var categories = await _categoryRepos.SearchCategoryByNameAsync(name);
            var modCategories = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                CategoryName = c.CategoryName
            });
            if (modCategories.Count() == 0)
                return new List<CategoryDTO>();
            return modCategories;
        }
    }
}
