using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.ProductModels;

namespace Egventoryv7BackEnd.Contracts.RepositoryContracts
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync();
        public Task<IEnumerable<Product>> GetAllAvailableProductsAsync();
        public Task<IEnumerable<Product>> GetProductByNameAsync(string name);
        public Task<Product> GetProductByIdAsync(int id);
        public Task<int> GetNumbersOfProductRegistered();
        public Task<int> GetNumbersOfCriticalProducts();
        public Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string category);
        public Task<IEnumerable<Product>> GetDisplayProductsAsync();
        public Task<bool> CreateProductAsync(Product request);
        public Task<bool> DeleteProductByIdAsync(int id);
        public Task<bool> UpdateProductAsync(Product request);
        public Task<bool> DuplicateProductValidation(string productName);
    }
}
