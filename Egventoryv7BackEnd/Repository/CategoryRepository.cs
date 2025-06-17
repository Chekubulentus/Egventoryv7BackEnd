using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.DataContext;
using Egventoryv7BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace Egventoryv7BackEnd.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PoultryContext _context;
        public CategoryRepository(PoultryContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
                return false;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> SearchCategoryByNameAsync(string name)
        {
            return await _context.Categories.Where(c => c.CategoryName.Contains(name)).ToListAsync();
        }
    }
}
