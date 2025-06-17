using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Models.DiscountModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;

namespace Egventoryv7BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService service)
        {
            _discountService = service;
        }
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<DiscountDTO>>> GetAllDiscountsAsync()
        {
            var discounts = await _discountService.GetAllDiscountsAsync();
            if (!discounts.Any() || discounts.Count() == 0)
                return NotFound("No Discounts currently registered.");
            return Ok(discounts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountDTO>> GetDiscountByIdAsync(int id)
        {
            if (id == 0)
                return BadRequest("Invalid request.");
            var discount = await _discountService.GetDiscountByIdAsync(id);
            if (discount is null)
                return NotFound("Discount does not exist.");
            return Ok(discount);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateDiscountAsync(CreateDiscountRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Discount Properties.");
            var validateDiscount = await _discountService.DiscountNameValidationAsync(request.DiscountName);
            if (!validateDiscount)
                return BadRequest("Discount already exist.");
            var createDiscount = await _discountService.CreateDiscountAsync(request);
            if (!createDiscount)
                return BadRequest("Discount cannot be created.");
            return Ok(createDiscount);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteDiscountByIdAsync(int id)
        {
            if (id == 0)
                return BadRequest("Discount does not exist.");
            var deleteDiscount = await _discountService.DeleteDiscountByIdAsync(id);
            if (!deleteDiscount)
                return BadRequest("Discount cannot be deleted.");
            return Ok(deleteDiscount);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateDiscountAsync(UpdateDiscountRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(request);
            var updateDiscount = await _discountService.UpdateDiscountAsync(request);
            if (!updateDiscount)
                return BadRequest("Discount cannot be updated.");
            return Ok(updateDiscount);
        }
        [HttpGet("Name")]
        public async Task<ActionResult<DiscountDTO>> GetDiscountByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return NotFound("Invalid entry.");
            var discount = await _discountService.GetDiscountByNameAsync(name);
            if (discount is null)
                return NotFound("Discount does not exist.");
            return Ok(discount);
        }
    }
}
