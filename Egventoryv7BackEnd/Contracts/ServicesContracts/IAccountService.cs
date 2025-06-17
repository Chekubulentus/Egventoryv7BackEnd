using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.AccountModels;

namespace Egventoryv7BackEnd.Contracts.ServicesContracts
{
    public interface IAccountService
    {
        public Task<IEnumerable<AccountDTO>> GetAllAccountsAsync();
        public Task<AccountDTO> GetAccountByIdAsync(int id);
        public Task<bool> CreateAccountAsync(CreateAccountRequest request);
        public Task<bool> DeleteAccountByIdAsync(int id);
        public Task<bool> UpdateAccountByIdAsync(UpdateAccountRequest request);
        public Task<bool> UsernameAccountValidationAsync(string userName);
        public Task<IEnumerable<AccountDTO>> GetAccountsByUsernameAsync(string userName);
    }
}
