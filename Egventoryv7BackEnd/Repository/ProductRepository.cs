using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.DataContext;
using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.ProductModels;
using Microsoft.EntityFrameworkCore;

namespace Egventoryv7BackEnd.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly PoultryContext _context;
        public ProductRepository(PoultryContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProductAsync(Product request)
        {
            await _context.Products.AddAsync(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
                return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DuplicateProductValidation(string productName)
        {
            return await _context.Products.AnyAsync(p => p.ProductName.Equals(productName));
        }

        public async Task<IEnumerable<Product>> GetAllAvailableProductsAsync()
        {
            return await _context.Products
                .Where(p => p.Status.Equals("Available"))
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetDisplayProductsAsync()
        {
            return await _context.Products
                .Take(9)
                .Include(p => p.Category)
                .OrderBy(p => p.ProductName)
                .ToListAsync();
        }

        public async Task<int> GetNumbersOfCriticalProducts()
        {
            return await _context.Products.CountAsync(p => p.StockQuantity <= 20);
        }

        public async Task<int> GetNumbersOfProductRegistered()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
                return null;
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.ProductName.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string category)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryName.Equals(category))
                .ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product request)
        {
            _context.Products.Update(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
