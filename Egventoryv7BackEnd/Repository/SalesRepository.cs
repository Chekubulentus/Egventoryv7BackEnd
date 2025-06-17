using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.DataContext;
using Egventoryv7BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace Egventoryv7BackEnd.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly PoultryContext _context;
        public SalesRepository(PoultryContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateSalesAsync(Sales request)
        {
            await _context.Sales.AddAsync(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeteleSalesByIdAsync(int id)
        {
            var sales = await _context.Sales.FindAsync(id);
            if (sales is null)
                return false;
            _context.Sales.Remove(sales);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Sales>> GetAllSalesAsync()
        {
            return await _context.Sales
                .Include(s => s.Employee)
                .Include(s => s.SalesItems)
                .OrderByDescending(s => s.PurchaseDate)
                .ToListAsync();
        }

        public async Task<double> GetDailySalesAmount()
        {
            var currentDate = DateTime.UtcNow.Date;

            var dailySales = await _context.Sales
                .Where(s => s.PurchaseDate.Date == currentDate)
                .ToListAsync();

            var totalSalesAmount = dailySales.Sum(s => s.TotalAmount);
            return Math.Round(totalSalesAmount);
        }

        public async Task<IEnumerable<Sales>> GetDailySalesAsync()
        {
            var currentDate = DateTime.UtcNow.Date;

            var sales = await _context.Sales.Where(s => s.PurchaseDate.Date == currentDate)
                .Include(s => s.SalesItems)
                .Include(s => s.Employee)
                .ToListAsync();
            return sales;
        }

        public async Task<Sales> GetSalesByIdAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale is null)
                return null;    
            return sale;
        }
        public async Task<bool> ReduceQuantityAsync(string productName, int quantity)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductName.Equals(productName));
            if (product is null || product.StockQuantity < quantity)
                return false;
            product.StockQuantity -= quantity;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
