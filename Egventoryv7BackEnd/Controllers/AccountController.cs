using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Models.AccountModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Egventoryv7BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accService;
        public AccountController(IAccountService service)
        {
            _accService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAllAccountsAsync()
        {
            var accounts = await _accService.GetAllAccountsAsync();
            if (accounts.Count() == 0)
                return BadRequest("No account currently registered.");
            return Ok(accounts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> GetAccountByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid entry.");
            var account = await _accService.GetAccountByIdAsync(id);
            if (account is null)
                return NotFound("Employee accounts does not exist.");
            return Ok(account);
        }
        [HttpGet("Username")]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccountsByUsernameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return BadRequest("Invalid username");
            var accounts = await _accService.GetAccountsByUsernameAsync(userName);
            if (!accounts.Any() || accounts.Count() == 0)
                return NotFound($"{userName} does not exist.");
            return Ok(accounts);
        }
        [HttpPost]
        public async Task<ActionResult<MessageResponse>> CreateAccountAsync(CreateAccountRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new MessageResponse { Message = "Invalid entry details."});
            var userNameValidation = await _accService.UsernameAccountValidationAsync(request.Username);
            if (!userNameValidation)
                return BadRequest(new MessageResponse { Message = "Username already taken." });
            var createUser = await _accService.CreateAccountAsync(request);
            if (!createUser)
                return BadRequest(new MessageResponse { Message = "Account cannot be created." });
            return Ok(new MessageResponse { Message = "Account created." });
        }
        [HttpDelete]
        public async Task<ActionResult<MessageResponse>> DeleteAccountByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest(new MessageResponse { Message = "Invalid entry." });
            var deleteAccount = await _accService.DeleteAccountByIdAsync(id);
            if (!deleteAccount)
                return BadRequest(new MessageResponse { Message = "Account does not exist." });
            return Ok(new MessageResponse { Message = "Account deleted." });
        }
        [HttpPut]
        public async Task<ActionResult<MessageResponse>> UpdateAccountAsync(UpdateAccountRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new MessageResponse { Message = "Invalid entry details" });
            var userNameValidation = await _accService.UsernameAccountValidationAsync(request.Username);
            if (!userNameValidation)
                return BadRequest(new MessageResponse { Message = "Username already taken" });
            var updateAccount = await _accService.UpdateAccountByIdAsync(request);
            if (!updateAccount)
                return BadRequest(new MessageResponse { Message = "Account cannot be updated." });
            return Ok(new MessageResponse { Message = "Employee Account updated." });
        }
    }
}
