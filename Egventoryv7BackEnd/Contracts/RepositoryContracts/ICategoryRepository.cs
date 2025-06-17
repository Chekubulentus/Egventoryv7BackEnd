using Egventoryv7BackEnd.Entities;

namespace Egventoryv7BackEnd.Contracts.RepositoryContracts
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
        public Task<Category> GetCategoryByIdAsync(int id);
        public Task<IEnumerable<Category>> SearchCategoryByNameAsync(string name);
        public Task<bool> CreateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryByIdAsync(int id);
    }
}
