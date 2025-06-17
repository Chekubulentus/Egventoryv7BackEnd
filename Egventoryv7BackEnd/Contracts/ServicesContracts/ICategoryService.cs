using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.CategoryModels;

namespace Egventoryv7BackEnd.Contracts.ServicesContracts
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        public Task<CategoryDTO> GetCategoryByIdAsync(int id);
        public Task<IEnumerable<CategoryDTO>> SearchCategoryByNameAsync(string name);
        public Task<bool> CreateCategoryAsync(CreateCategoryRequest category);
        public Task<bool> DeleteCategoryByIdAsync(int id);
    }
}
