using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.SalesItemModels;

namespace Egventoryv7BackEnd.Contracts.ServicesContracts
{
    public interface ISalesItemService
    {
        public Task<IEnumerable<SalesItemDTO>> GetAllSalesItemsAsync();
        public Task<IEnumerable<SalesItemDTO>> GetSalesItemBySalesId(int salesId);
        public Task<SalesItemDTO> GetSalesItemByIdAsync(int id);
        public Task<bool> CreateSalesItemAsync(List<CreateSalesItemsRequest> request);
        public Task<bool> DeleteSalesItemByIdAsync(int id);
    }
}
