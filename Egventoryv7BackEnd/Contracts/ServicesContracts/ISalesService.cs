using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.SalesModels;

namespace Egventoryv7BackEnd.Contracts.ServicesContracts
{
    public interface ISalesService
    {
        public Task<IEnumerable<SalesDTO>> GetAllSalesAsync();
        public Task<SalesDTO> GetSalesByIdAsync(int id);
        public Task<IEnumerable<SalesDTO>> GetDailySalesAsync();
        public Task<bool> CreateSalesAsync(CreateSaleRequest request);
        public Task<bool> DeleteSalesByIdAsync(int id);
        public Task<bool> TransactionAsync(TransactionRequest request);
        public Task<bool> ReduceQuantityAsync(string productName, int quantity);
        public Task<double> GetDailySalesAmount();
    }
}
