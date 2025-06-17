using Egventoryv7BackEnd.DataContext;
using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.AuthenticationModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Egventoryv7BackEnd.Services
{
    public class AuthenticationService
    {
        private readonly PoultryContext _context;
        private readonly IConfiguration _config;
        public AuthenticationService(PoultryContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }
        
        public string TokenGeneration(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, account.Employee.Role.RolePosition),
                new Claim("EmployeeId", account.EmployeeId.ToString())
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:SecretKey")));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

            var tokenDescription = new JwtSecurityToken(
                issuer: _config.GetValue<string>("Jwt:Issuer"),
                audience: _config.GetValue<string>("Jwt:Audience"),
                expires: DateTime.UtcNow.AddHours(_config.GetValue<int>("Jwt:TokenValidity")),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescription);
        }

        public async Task<string> Authentication(LogInRequest request)
        {
            var account = await _context.Accounts
                .Include(a => a.Employee)
                .ThenInclude(e => e.Role)
                .FirstOrDefaultAsync(a => a.Username.Equals(request.Username));
            if (account is null)
                return null;
            if (!account.HashedPassword.Equals(request.Password))
                return null;
            string token = TokenGeneration(account);
            return token;
        }
    }
}
