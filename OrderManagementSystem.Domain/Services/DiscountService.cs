using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Domain.Services
{
    public class DiscountService
    {
        private readonly IEnumerable<IDiscountStrategy> _discountStrategies;

        public DiscountService(IEnumerable<IDiscountStrategy> discountStrategies)
        {
            _discountStrategies = discountStrategies;
        }

        public async Task ApplyDiscountsAsync(Order order)
        {
            var applicableStrategies = _discountStrategies
                .Where(s => s.IsApplicable(order))
                .ToList();

            if (!applicableStrategies.Any())
                return;

            var maxDiscount = await Task.WhenAll(
                applicableStrategies.Select(s => s.CalculateDiscountAsync(order)));

            var highestDiscount = maxDiscount.Max();
            order.ApplyDiscount(highestDiscount);
        }
    }
} 