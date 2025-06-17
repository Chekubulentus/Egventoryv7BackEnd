using Egventoryv7BackEnd.Entities;
using System.Runtime.CompilerServices;

namespace Egventoryv7BackEnd.Contracts.RepositoryContracts
{
    public interface ISalesItemRepository
    {
        public Task<IEnumerable<SalesItem>> GetAllSalesItemsAsync();
        public Task<IEnumerable<SalesItem>> GetSalesItemBySalesId(int salesId);
        public Task<SalesItem> GetSalesItemByIdAsync(int id);
        public Task<bool> CreateSalesItemAsync(List<SalesItem> request);
        public Task<bool> DeleteSalesItemByIdAsync(int id);
    }
}
