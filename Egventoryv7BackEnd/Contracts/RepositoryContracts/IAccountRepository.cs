using Egventoryv7BackEnd.Entities;

namespace Egventoryv7BackEnd.Contracts.RepositoryContracts
{
    public interface IAccountRepository
    {
        public Task<IEnumerable<Account>> GetAllAccountsAsync();
        public Task<Account> GetAccountByIdAsync(int id);
        public Task<IEnumerable<Account>> GetAccountsByUsernameAsync(string userName);
        public Task<bool> CreateAccountAsync(Account request);
        public Task<bool> DeleteAccountByIdAsync(int id);
        public Task<bool> UpdateAccountByIdAsync(Account request);
        public Task<bool> UsernameAccountValidationAsync(string userName);
    }
}
