using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Models.AccountModels;
using Egventoryv7BackEnd.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Egventoryv7BackEnd.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accRepos;
        public AccountService(IAccountRepository repos)
        {
            _accRepos = repos;
        }

        public async Task<bool> CreateAccountAsync(CreateAccountRequest request)
        {
            var newAccount = new Account
            {
                Username = request.Username,
                HashedPassword = request.Password,
                EmployeeId = request.EmployeeId
            };
            var createAccount = await _accRepos.CreateAccountAsync(newAccount);
            if (!createAccount)
                return false;
            return createAccount;
        }

        public async Task<bool> DeleteAccountByIdAsync(int id)
        {
            var deleteAccount = await _accRepos.DeleteAccountByIdAsync(id);
            if (!deleteAccount)
                return false;
            return deleteAccount;
        }

        public async Task<AccountDTO> GetAccountByIdAsync(int id)
        {
            var account = await _accRepos.GetAccountByIdAsync(id);
            var modAccount = new AccountDTO
            {
                Id = account.Id,
                Username = account.Username,
                Password = account.HashedPassword,
                EmployeeId = account.EmployeeId
            };
            return modAccount;
        }

        public async Task<IEnumerable<AccountDTO>> GetAccountsByUsernameAsync(string userName)
        {
            var accounts = await _accRepos.GetAccountsByUsernameAsync(userName);
            if (!accounts.Any() || accounts.Count() == 0)
                return null;
            var modAccounts = accounts.Select(a => new AccountDTO
            {
                Id = a.Id,
                Username = a.Username,
                Password = a.HashedPassword,
                EmployeeId = a.EmployeeId
            });
            return modAccounts;
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAccountsAsync()
        {
            var accounts = await _accRepos.GetAllAccountsAsync();
            var modAccounts = accounts.Select(a => new AccountDTO
            {
                Id = a.Id,
                Username = a.Username,
                Password = a.HashedPassword,
                EmployeeId = a.EmployeeId
            });
            return modAccounts;
        }

        public async Task<bool> UpdateAccountByIdAsync(UpdateAccountRequest request)
        {
            var accountToUpdate = await _accRepos.GetAccountByIdAsync(request.Id);
            if (accountToUpdate is null)
                return false;
            var hashedPassword = new PasswordHasher<UpdateAccountRequest>()
                .HashPassword(request, request.Password);
            accountToUpdate.Username = request.Username;
            accountToUpdate.HashedPassword = hashedPassword;

            var updateAccount = await _accRepos.UpdateAccountByIdAsync(accountToUpdate);
            if (!updateAccount)
                return false;
            return updateAccount;
        }

        public async Task<bool> UsernameAccountValidationAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return false;
            var userNameValidation = await _accRepos.UsernameAccountValidationAsync(userName);
            if (!userNameValidation)
                return false;
            return true;
        }
    }
}
