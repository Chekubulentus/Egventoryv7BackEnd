using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Models.ProductModels;
using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Egventoryv7BackEnd.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepos;
        private readonly PoultryContext _context;
        public ProductService(IProductRepository repos, PoultryContext context)
        {
            _productRepos = repos;
            _context = context;
        }
        public async Task<bool> CreateProductAsync(CreateProductRequest request)
        {
            if (request is null)
                return false;
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName.Equals(request.CategoryName));
            if (category is null)
                return false;
            var newProduct = new Product
            {
                ProductName = request.ProductName,
                Price = request.Price,
                ExpiryDate = request.ExpiryDate,
                StockQuantity = request.StockQuantity,
                Status = request.Status,
                CategoryId = category.Id
            };
            var createProduct = await _productRepos.CreateProductAsync(newProduct);
            if (!createProduct)
                return false;
            return createProduct;
        }

        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            var deleteProduct = await _productRepos.DeleteProductByIdAsync(id);
            if (!deleteProduct)
                return false;
            return deleteProduct;
        }

        public async Task<bool> DuplicateProductValidation(string productName)
        {
            return await _productRepos.DuplicateProductValidation(productName);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAvailableProductsAsync()
        {
            var products = await _productRepos.GetAllAvailableProductsAsync();
            if (!products.Any() || products.Count() == 0)
                return new List<ProductDTO>();
            var modProducts = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                ExpiryDate = p.ExpiryDate,
                StockQuantity = p.StockQuantity,
                Status = p.Status,
                CategoryName = p.Category.CategoryName
            });
            return modProducts;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepos.GetAllProductsAsync();
            var modProducts = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                ExpiryDate = p.ExpiryDate,
                StockQuantity = p.StockQuantity,
                Status = p.Status,
                CategoryName = p.Category.CategoryName
            });
            return modProducts;
        }

        public async Task<IEnumerable<ProductDTO>> GetDisplayProductsAsync()
        {
            var products = await _productRepos.GetDisplayProductsAsync();
            if (!products.Any() || products.Count() == 0)
                return new List<ProductDTO>();
            var modProducts = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                ExpiryDate = p.ExpiryDate,
                StockQuantity = p.StockQuantity,
                Status = p.Status,
                CategoryName = p.Category.CategoryName
            });
            return modProducts;
        }

        public async Task<int> GetNumbersOfCriticalProducts()
        {
            var criticalProducts = await _productRepos.GetNumbersOfCriticalProducts();
            if (criticalProducts <= 0)
                return 0;
            return criticalProducts;
        }

        public async Task<int> GetNumbersOfProductRegistered()
        {
            var products = await _productRepos.GetNumbersOfProductRegistered();
            if (products <= 0)
                return 0;
            return products;
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _productRepos.GetProductByIdAsync(id);
            if (product is null)
                return null;
            var modProduct = new ProductDTO
            {
                ProductName = product.ProductName,
                Price = product.Price,
                ExpiryDate = product.ExpiryDate,
                StockQuantity = product.StockQuantity,
                Status = product.Status,
                CategoryName = product.Category.CategoryName
            };
            return modProduct;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductByNameAsync(string name)
        {
            var product = await _productRepos.GetProductByNameAsync(name);
            var modProducts = product.Select(p => new ProductDTO
            {
                ProductName = p.ProductName,
                Price = p.Price,
                ExpiryDate = p.ExpiryDate,
                StockQuantity = p.StockQuantity,
                Status = p.Status,
                CategoryName = p.Category.CategoryName
            });
            if (modProducts.Count() <= 0)
                return new List<ProductDTO>();
            return modProducts;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryNameAsync(string category)
        {
            var products = await _productRepos.GetProductsByCategoryNameAsync(category);
            if (!products.Any() || products.Count() == 0)
                return new List<ProductDTO>();
            var modProducts = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                ExpiryDate = p.ExpiryDate,
                StockQuantity = p.StockQuantity,
                Status = p.Status,
                CategoryName = p.Category.CategoryName
            });
            return modProducts;
        }

        public async Task<bool> UpdateProductAsync(UpdateProductRequest request)
        {
            var productToUpdate = await _productRepos.GetProductByIdAsync(request.Id);
            if (productToUpdate is null)
                return false;
            var categoryId = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName.Equals(request.CategoryName));

            productToUpdate.ProductName = request.ProductName;
            productToUpdate.Price = request.Price;
            productToUpdate.ExpiryDate = request.ExpiryDate;
            productToUpdate.StockQuantity = request.StockQuantity;
            productToUpdate.Status = request.Status;
            productToUpdate.CategoryId = categoryId.Id;

            var updateProduct = await _productRepos.UpdateProductAsync(productToUpdate);
            if (!updateProduct)
                return false;
            return updateProduct;
        }
    }
}
