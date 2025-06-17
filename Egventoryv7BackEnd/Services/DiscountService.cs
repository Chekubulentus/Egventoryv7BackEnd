using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.DiscountModels;

namespace Egventoryv7BackEnd.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<bool> CreateDiscountAsync(CreateDiscountRequest request)
        {
            if (request is null)
                return false;
            var discount = new Discount
            {
                DiscountName = request.DiscountName,
                DiscountRate = request.DiscountRate
            };
            var createDiscount = await _discountRepository.CreateDiscountAsync(discount);
            if (!createDiscount)
                return false;
            return createDiscount;
        }

        public async Task<bool> DeleteDiscountByIdAsync(int id)
        {
            if (id == 0)
                return false;
            var deleteDiscount = await _discountRepository.DeleteDiscountByIdAsync(id);
            if (!deleteDiscount)
                return false;
            return deleteDiscount;
        }

        public async Task<bool> DiscountNameValidationAsync(string discountName)
        {
            if (string.IsNullOrEmpty(discountName))
                return false;
            var validateDiscount = await _discountRepository.DiscountNameValidationAsync(discountName);
            if (validateDiscount)
                return false;
            return true;
        }

        public async Task<IEnumerable<DiscountDTO>> GetAllDiscountsAsync()
        {
            var discounts = await _discountRepository.GetAllDiscountsAsync();
            if (!discounts.Any() || discounts.Count() == 0)
                return new List<DiscountDTO>();
            var modDiscounts = discounts.Select(d => new DiscountDTO
            {
                Id = d.Id,
                DiscountName = d.DiscountName,
                DiscountRate = d.DiscountRate
            });
            return modDiscounts;
        }

        public async Task<DiscountDTO> GetDiscountByIdAsync(int id)
        {
            if (id == 0)
                return null;
            var discount = await _discountRepository.GetDiscountByIdAsync(id);
            if (discount is null)
                return null;
            var modDiscount = new DiscountDTO
            {
                Id = discount.Id,
                DiscountName = discount.DiscountName,
                DiscountRate = discount.DiscountRate
            };
            return modDiscount;
        }

        public async Task<IEnumerable<DiscountDTO>> GetDiscountByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            var discount = await _discountRepository.GetDiscountByNameAsync(name);
            if (discount is null)
                return null;
            var modDiscount = discount.Select(d => new DiscountDTO
            {
                DiscountName = d.DiscountName,
                DiscountRate = d.DiscountRate
            });
            return modDiscount;
        }

        public async Task<bool> UpdateDiscountAsync(UpdateDiscountRequest request)
        {
            if (request is null)
                return false;
            var discountToUpdate = await _discountRepository.GetDiscountByIdAsync(request.Id);
            if (discountToUpdate is null)
                return false;
            discountToUpdate.DiscountName = request.DiscountName;
            discountToUpdate.DiscountRate = request.DiscountRate;
            var updateDiscount = await _discountRepository.UpdateDiscountAsync(discountToUpdate);
            if (!updateDiscount)
                return false;
            return updateDiscount;
        }
    }
}
