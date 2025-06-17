using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.DataContext;
using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.SalesModels;
using System.Diagnostics;

namespace Egventoryv7BackEnd.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly ISalesItemRepository _salesItemRepository;
        private readonly PoultryContext _context;
        public SalesService(ISalesRepository repository, ISalesItemRepository salesItemRepository, PoultryContext context)
        {
            _salesItemRepository = salesItemRepository;
            _salesRepository = repository;
            _context = context;
        }

        public async Task<bool> CreateSalesAsync(CreateSaleRequest request)
        {
            if (request is null)
                return false;
            var sale = new Sales
            {
                PurchaseDate = DateTime.UtcNow,
                EmployeeId = request.EmployeeId,
                TotalAmount = request.TotalAmount,
                SalesItems = request.SalesItems
            };
            var createSale = await _salesRepository.CreateSalesAsync(sale);
            if (!createSale)
                return false;
            return true;
        }

        public async Task<bool> DeleteSalesByIdAsync(int id)
        {
            if (id <= 0)
                return false;
            var deleteSale = await _salesRepository.DeteleSalesByIdAsync(id);
            if (!deleteSale)
                return false;
            return deleteSale;
        }

        public async Task<IEnumerable<SalesDTO>> GetAllSalesAsync()
        {
            var sales = await _salesRepository.GetAllSalesAsync();
            if (!sales.Any() || sales.Count() == 0)
                return new List<SalesDTO>();
            var modSales = sales.Select(s => new SalesDTO
            {
                Id = s.Id,
                PurchaseDate = s.PurchaseDate,
                CashierName = s.Employee.Name,
                SalesItems = s.SalesItems,
                TotalAmount = s.TotalAmount
            });
            return modSales;
        }

        public async Task<SalesDTO> GetSalesByIdAsync(int id)
        {
            if (id <= 0)
                return null;
            var sale = await _salesRepository.GetSalesByIdAsync(id);
            if (sale is null)
                return null;
            var modSale = new SalesDTO
            {
                Id = sale.Id,
                PurchaseDate = sale.PurchaseDate,
                CashierName = sale.Employee.Name,
                SalesItems = sale.SalesItems,
                TotalAmount = sale.TotalAmount
            };
            return modSale;
        }
        public async Task<bool> TransactionAsync(TransactionRequest request)
        {
            if (request is null)
                return false;
            var baseAmount = request.SalesItems.Sum(si => si.SubTotal);
            var totalAmount = baseAmount * 1.12;

            var modSalesItems = request.SalesItems.Select(si => new SalesItem
            {
                ProductName = si.ProductName,
                Category = si.Category,
                Price = si.Price,
                Quantity = si.Quantity,
                SubTotal = si.SubTotal,
                Discount = si.Discount
            }).ToList();

            var sale = new Sales
            {
                PurchaseDate = DateTime.UtcNow,
                EmployeeId = request.EmployeeId,
                TotalAmount = totalAmount,
                SalesItems = modSalesItems
            };

            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();

            foreach (var item in modSalesItems)
            {
                item.SalesId = sale.Id;
            }

            for (int i = 0; i < request.SalesItems.Count; i++)
            {
                var success = await _salesRepository.ReduceQuantityAsync(
                request.SalesItems[i].ProductName,
                request.SalesItems[i].Quantity
                );
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> ReduceQuantityAsync(string productName, int quantity)
        {
            if (string.IsNullOrEmpty(productName) || quantity < 0)
                return false;
            var reduceQuantity = await _salesRepository.ReduceQuantityAsync(productName, quantity);
            if (!reduceQuantity)
                return false;
            return true;
        }

        public async Task<IEnumerable<SalesDTO>> GetDailySalesAsync()
        {
            var getDailySales = await _salesRepository.GetDailySalesAsync();
            if (!getDailySales.Any() || getDailySales.Count() == 0)
                return new List<SalesDTO>();
            var modDailySales = getDailySales.Select(s => new SalesDTO
            {
                Id = s.Id,
                PurchaseDate = s.PurchaseDate,
                CashierName = s.Employee.Name,
                SalesItems = s.SalesItems,
                TotalAmount = s.TotalAmount
            });
            return modDailySales;
        }

        public async Task<double> GetDailySalesAmount()
        {
            var dailySalesAmount = await _salesRepository.GetDailySalesAmount();
            if (dailySalesAmount == 0)
                return 0;
            return dailySalesAmount;
        }
    }
}
