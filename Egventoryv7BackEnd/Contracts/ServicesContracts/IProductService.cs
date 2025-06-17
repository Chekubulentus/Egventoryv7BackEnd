using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.ProductModels;

namespace Egventoryv7BackEnd.Contracts.ServicesContracts
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        public Task<IEnumerable<ProductDTO>> GetAllAvailableProductsAsync();
        public Task<IEnumerable<ProductDTO>> GetProductByNameAsync(string name);
        public Task<ProductDTO> GetProductByIdAsync(int id);
        public Task<int> GetNumbersOfProductRegistered();
        public Task<int> GetNumbersOfCriticalProducts();
        public Task<IEnumerable<ProductDTO>> GetProductsByCategoryNameAsync(string category);
        public Task<IEnumerable<ProductDTO>> GetDisplayProductsAsync();
        public Task<bool> CreateProductAsync(CreateProductRequest request);
        public Task<bool> DeleteProductByIdAsync(int id);
        public Task<bool> UpdateProductAsync(UpdateProductRequest request);
        public Task<bool> DuplicateProductValidation(string productName);
    }
}
