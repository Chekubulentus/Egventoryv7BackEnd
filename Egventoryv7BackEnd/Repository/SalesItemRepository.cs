using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.DataContext;
using Egventoryv7BackEnd.Entities;
using Microsoft.EntityFrameworkCore;
using Egventoryv7BackEnd.Models.SalesItemModels;

namespace Egventoryv7BackEnd.Repository
{
    public class SalesItemRepository : ISalesItemRepository
    {
        private readonly PoultryContext _context;
        public SalesItemRepository(PoultryContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateSalesItemAsync(List<SalesItem> request)
        {
            await _context.SalesItems.AddRangeAsync(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSalesItemByIdAsync(int id)
        {
            var salesItem = await _context.SalesItems.FindAsync(id);
            if (salesItem is null)
                return false;
            _context.SalesItems.Remove(salesItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SalesItem>> GetAllSalesItemsAsync()
        {
            return await _context.SalesItems
                .Include(si => si.Sales)
                .ToListAsync();
        }

        public async Task<SalesItem> GetSalesItemByIdAsync(int id)
        {
            return await _context.SalesItems
                .Include(si => si.Sales)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SalesItem>> GetSalesItemBySalesId(int id)
        {
            return await _context.SalesItems
                .Include(si => si.Sales)
                .Where(si => si.SalesId == id)
                .ToListAsync();
        }
    }
}
