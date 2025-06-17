using Egventoryv7BackEnd.Entities;

namespace Egventoryv7BackEnd.Contracts.RepositoryContracts
{
    public interface IDiscountRepository
    {
        public Task<IEnumerable<Discount>> GetAllDiscountsAsync();
        public Task<Discount> GetDiscountByIdAsync(int id);
        public Task<bool> CreateDiscountAsync(Discount request);
        public Task<bool> DeleteDiscountByIdAsync(int id);
        public Task<bool> UpdateDiscountAsync(Discount request);
        public Task<bool> DiscountNameValidationAsync(string discountName);
        public Task<IEnumerable<Discount>> GetDiscountByNameAsync(string name);
    }
}
