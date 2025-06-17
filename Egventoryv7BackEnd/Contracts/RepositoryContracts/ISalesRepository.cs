using Egventoryv7BackEnd.Entities;

namespace Egventoryv7BackEnd.Contracts.RepositoryContracts
{
    public interface ISalesRepository
    {
        public Task<IEnumerable<Sales>> GetAllSalesAsync();
        public Task<Sales> GetSalesByIdAsync(int id);
        public Task<IEnumerable<Sales>> GetDailySalesAsync();
        public Task<bool> CreateSalesAsync(Sales request);
        public Task<bool> DeteleSalesByIdAsync(int id);
        public Task<bool> ReduceQuantityAsync(string productName, int quantity);
        public Task<double> GetDailySalesAmount();
    }
}
