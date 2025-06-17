    using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.SalesItemModels;
using Microsoft.AspNetCore.Connections.Features;

namespace Egventoryv7BackEnd.Services
{
    public class SalesItemService : ISalesItemService
    {
        private readonly ISalesItemRepository _salesRepos;

        public SalesItemService(ISalesItemRepository repos)
        {
            _salesRepos = repos;
        }

        public async Task<bool> CreateSalesItemAsync(List<CreateSalesItemsRequest> request)
        {
            if (!request.Any() || request.Count() == 0)
                return false;
            var newSalesItems = request.Select(si => new SalesItem
            {
                ProductName = si.ProductName,
                Price = si.Price,
                Quantity = si.Quantity,
            }).ToList();
            var createSalesItems = await _salesRepos.CreateSalesItemAsync(newSalesItems);
            if (!createSalesItems)
                return false;
            return createSalesItems;
        }

        public async Task<bool> DeleteSalesItemByIdAsync(int id)
        {
            if (id <= 0)
                return false;
            var deleteSalesItem = await _salesRepos.DeleteSalesItemByIdAsync(id);
            if (!deleteSalesItem)
                return false;
            return deleteSalesItem;
        }

        public async Task<IEnumerable<SalesItemDTO>> GetAllSalesItemsAsync()
        {
            var salesItems = await _salesRepos.GetAllSalesItemsAsync();
            if (!salesItems.Any() || salesItems.Count() == 0)
                return new List<SalesItemDTO>();
            var modSalesItems = salesItems.Select(si => new SalesItemDTO
            {
                ProductName = si.ProductName,
                Price = si.Price,
                Quantity = si.Quantity,

            });
            return modSalesItems;
        }

        public async Task<SalesItemDTO> GetSalesItemByIdAsync(int id)
        {
            var salesItem = await _salesRepos.GetSalesItemByIdAsync(id);
            if (salesItem is null)
                return null;
            var modSalesItem = new SalesItemDTO
            {
                ProductName = salesItem.ProductName,
                Price = salesItem.Price,
                Quantity = salesItem.Quantity
            };
            return modSalesItem;
        }

        public async Task<IEnumerable<SalesItemDTO>> GetSalesItemBySalesId(int salesId)
        {
            var salesItems = await _salesRepos.GetSalesItemBySalesId(salesId);
            if (!salesItems.Any() || salesItems.Count() == 0)
                return new List<SalesItemDTO>();
            var modSalesItems = salesItems.Select(si => new SalesItemDTO
            {
                ProductName = si.ProductName,
                Price = si.Price,
                Quantity = si.Quantity,
                Discount = si.Discount
            });
            return modSalesItems;
        }
    }
}
