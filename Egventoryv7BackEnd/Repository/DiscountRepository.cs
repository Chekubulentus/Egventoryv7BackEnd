using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.DataContext;
using Egventoryv7BackEnd.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Egventoryv7BackEnd.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly PoultryContext _context;
        public DiscountRepository(PoultryContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateDiscountAsync(Discount request)
        {
            await _context.Discounts.AddAsync(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDiscountByIdAsync(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount is null)
                return false;
            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DiscountNameValidationAsync(string discountName)
        {
            return await _context.Discounts.AnyAsync(d => d.DiscountName.Equals(discountName));
        }

        public async Task<IEnumerable<Discount>> GetAllDiscountsAsync()
        {
            return await _context.Discounts.ToListAsync();  
        }

        public async Task<Discount> GetDiscountByIdAsync(int id)
        {
            return await _context.Discounts.FindAsync(id);
        }

        public async Task<IEnumerable<Discount>> GetDiscountByNameAsync(string name)
        {
            return await _context.Discounts.Where(d => d.DiscountName.Equals(name)).ToListAsync();
        }

        public async Task<bool> UpdateDiscountAsync(Discount request)
        {
            _context.Discounts.Update(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
