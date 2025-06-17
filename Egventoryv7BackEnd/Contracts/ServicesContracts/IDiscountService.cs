using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.DiscountModels;

namespace Egventoryv7BackEnd.Contracts.ServicesContracts
{
    public interface IDiscountService
    {
        public Task<IEnumerable<DiscountDTO>> GetAllDiscountsAsync();
        public Task<DiscountDTO> GetDiscountByIdAsync(int id);
        public Task<bool> CreateDiscountAsync(CreateDiscountRequest request);
        public Task<bool> DeleteDiscountByIdAsync(int id);
        public Task<bool> UpdateDiscountAsync(UpdateDiscountRequest request);
        public Task<bool> DiscountNameValidationAsync(string discountName);
        public Task<IEnumerable<DiscountDTO>> GetDiscountByNameAsync(string name);
    }
}
