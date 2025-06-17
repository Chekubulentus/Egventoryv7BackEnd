using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Models.ProductModels;
using Egventoryv7BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Egventoryv7BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProductsAsync()
        {
            var products = await _service.GetAllProductsAsync();
            if (!products.Any() || products.Count() == 0)
                return NotFound("No Product is currently registered.");
            return Ok(products);
        }
        [HttpGet("Available")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllAvailableProductsAsync()
        {
            var products = await _service.GetAllAvailableProductsAsync();
            if (!products.Any() || products.Count() == 0)
                return NotFound("No products available.");
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid entry.");
            var product = await _service.GetProductByIdAsync(id);
            if (product is null)
                return NotFound("Product does not exist.");
            return Ok(product);
        }
        [HttpGet("Name")]
        public async Task<ActionResult<ProductDTO>> GetProductsByNameAsync(string productName)
        {
            if (string.IsNullOrEmpty(productName))
                return BadRequest("Invalid entry.");
            var products = await _service.GetProductByNameAsync(productName);
            if (!products.Any() || products.Count() == 0)
                return BadRequest($"There is no product with the name {productName}");
            return Ok(products);
        }
        [HttpGet("ProductsRegistered")]
        public async Task<ActionResult<int>> GetNumbersOfRegisteredProducts()
        {
            var products = await _service.GetNumbersOfProductRegistered();
            return Ok(products);
        }
        [HttpGet("CriticalProducts")]
        public async Task<ActionResult<int>> GetNumbersOfCriticalProducts()
        {
            var criticalProducts = await _service.GetNumbersOfCriticalProducts();
            return Ok(criticalProducts);
        }
        [HttpGet("Category")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategoryNameAsync(string category)
        {
            var products = await _service.GetProductsByCategoryNameAsync(category);
            if (!products.Any() || products.Count() == 0)
                return NotFound("No products currently registered.");
            return Ok(products);
        }
        [HttpGet("Display")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetDisplayProductsAsync()
        {
            var products = await _service.GetDisplayProductsAsync();
            if (!products.Any() || products.Count() == 0)
                return NotFound("No products currently registered");
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult<ProductMessageResponse>> CreateProductAsync(CreateProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ProductMessageResponse { Message = "Invalid entry details." });

            var duplicateValidation = await _service.DuplicateProductValidation(request.ProductName);
            if (duplicateValidation)
                return BadRequest(new ProductMessageResponse { Message = "Product already exists." });

            var createProduct = await _service.CreateProductAsync(request);
            if (!createProduct)
                return BadRequest(new ProductMessageResponse { Message = "Product cannot be created." });

            return Ok(new ProductMessageResponse { Message = "Product added." });
        }
        [HttpDelete]
        public async Task<ActionResult<ProductMessageResponse>> DeleteProductByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest(new ProductMessageResponse { Message = "Invalid id" });
            var deleteProduct = await _service.DeleteProductByIdAsync(id);
            if (!deleteProduct)
                return BadRequest(new ProductMessageResponse { Message = "Product cannot be deleted." });
            return Ok(new ProductMessageResponse { Message = "Product deleted." });
        }
        [HttpPut]
        public async Task<ActionResult<ProductMessageResponse>> UpdateProductAsync(UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ProductMessageResponse { Message = "Invalid entry details" });
            var updateProduct = await _service.UpdateProductAsync(request);
            if (!updateProduct)
                return BadRequest(new ProductMessageResponse { Message = "Product cannot be updated." });
            return Ok(new ProductMessageResponse { Message = "Product updated." });
        }
    }

}
