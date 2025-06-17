using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.DataContext;
using Egventoryv7BackEnd.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Egventoryv7BackEnd.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PoultryContext _context;
        public AccountRepository(PoultryContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAccountAsync(Account request)
        {
            await _context.Accounts.AddAsync(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAccountByIdAsync(int id)
        {
            var account = await _context.Accounts
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (account.Employee != null)
                _context.Employees.Remove(account.Employee);
            if (account is null)
                return false;
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Account>> GetAccountsByUsernameAsync(string userName)
        {
            return await _context.Accounts.Where(a => a.Username.Contains(userName)).ToListAsync();
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<bool> UpdateAccountByIdAsync(Account request)
        {
            _context.Accounts.Update(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UsernameAccountValidationAsync(string userName)
        {
            var userNameValidation = await _context.Accounts.AnyAsync(a => a.Username.Equals(userName));
            if (!userNameValidation)
                return true;
            return false;
        }
    }
}
