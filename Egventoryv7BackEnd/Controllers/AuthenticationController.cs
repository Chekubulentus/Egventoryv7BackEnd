using Egventoryv7BackEnd.Models.AccountModels;
using Egventoryv7BackEnd.Models.AuthenticationModels;
using Egventoryv7BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egventoryv7BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authService;
        public AuthenticationController(AuthenticationService service)
        {
            _authService = service;
        }
        [HttpPost]
        public async Task<ActionResult<LogInResponse>> LogInAsync(LogInRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new LogInResponse { Token = null, Message = "Invalid entry." });

            var token = await _authService.Authentication(request);
            if (token is null)
                return BadRequest(new LogInResponse { Token = null, Message = "Invalid credentials" });
            return Ok(new LogInResponse { Token = token, Message = "Logged In" });
        }
    }
}
