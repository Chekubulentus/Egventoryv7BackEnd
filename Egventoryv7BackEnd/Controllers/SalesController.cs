using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Models.SalesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace Egventoryv7BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService; 
        public SalesController(ISalesService service)
        {
            _salesService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesDTO>>> GetAllSalesAsync()
        {
            var sales = await _salesService.GetAllSalesAsync();
            if (!sales.Any() || sales.Count() <= 0)
                return NotFound("No Sales currently recorded.");
            return Ok(sales);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesDTO>> GetSaleByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid request.");
            var sale = await _salesService.GetSalesByIdAsync(id);
            if (sale is null)
                return NotFound("Sale does not exist.");
            return Ok(sale);
        }
        [HttpGet("DailySales")]
        public async Task<ActionResult<IEnumerable<SalesDTO>>> GetDailySalesAsync()
        {
            var getDailySales = await _salesService.GetDailySalesAsync();
            if (!getDailySales.Any() || getDailySales.Count() == 0)
                return NotFound("No transactions have been made today.");
            return Ok(getDailySales);
        }
        [HttpGet("DailySalesAmount")]
        public async Task<ActionResult<double>> GetDailySalesAmountAsync()
        {
            var dailySalesAmount = await _salesService.GetDailySalesAmount();
            if (dailySalesAmount == 0)
                return BadRequest("No transaction happened for today.");
            return Ok(dailySalesAmount);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateSalesAsync(CreateSaleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid sale.");
            var createSales = await _salesService.CreateSalesAsync(request);
            if (!createSales)
                return BadRequest("Sale cannot be created.");
            return Ok(createSales);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteSaleByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Sale cannot be deleeted due to invalid identifier.");
            var deleteSale = await _salesService.DeleteSalesByIdAsync(id);
            if (!deleteSale)
                return BadRequest("Sale cannot be deleted.");
            return Ok(deleteSale);
        }
        [HttpPost("Transaction")]
        public async Task<ActionResult<bool>> TransactionAsync(TransactionRequest request)
        {
            if (request is null || !request.SalesItems.Any())
                return BadRequest("Transaction failed.");
            var transaction = await _salesService.TransactionAsync(request);
            if (!transaction)
                return BadRequest("Transaction failed.");
            return Ok(transaction);
        }
    }
}
