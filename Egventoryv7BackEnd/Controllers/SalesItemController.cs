using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Models.SalesItemModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Egventoryv7BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesItemController : ControllerBase
    {
        private readonly ISalesItemService _salesItemService;
        public SalesItemController(ISalesItemService service)
        {
            _salesItemService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesItemDTO>>> GetAllSalesItemsAsync()
        {
            var salesItems = await _salesItemService.GetAllSalesItemsAsync();
            if (!salesItems.Any() || salesItems.Count() == 0)
                return NotFound("No sales items currently exist.");
            return Ok(salesItems);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesItemDTO>> GetSalesItemByIdAsync(int id)
        {
            var salesItem = await _salesItemService.GetSalesItemByIdAsync(id);
            if (salesItem is null)
                return NotFound("Sales Item does not exist.");
            return Ok(salesItem);
        }
        [HttpGet("SalesId")]
        public async Task<ActionResult<IEnumerable<SalesItemDTO>>> GetSalesItemsBySalesIdAsync(int salesId)
        {
            if (salesId <= 0)
                return BadRequest("Invalid Sales Id");
            var salesItems = await _salesItemService.GetSalesItemBySalesId(salesId);
            if (!salesItems.Any() || salesItems.Count() == 0)
                return NotFound("No Sales Items current exist.");
            return Ok(salesItems);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateSalesItemAsync(List<CreateSalesItemsRequest> request)
        {
            if (!request.Any() || request.Count() == 0)
                return BadRequest("No items registered.");
            var createSalesItems = await _salesItemService.CreateSalesItemAsync(request);
            if (!createSalesItems)
                return BadRequest("Sales Items cannot be committed.");
            return Ok(createSalesItems);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteSalesItemByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");
            var deleteSalesItem = await _salesItemService.DeleteSalesItemByIdAsync(id);
            if (!deleteSalesItem)
                return BadRequest("Sales Item cannot be deleted.");
            return Ok(deleteSalesItem);
        }
    }
}
