using System.Threading.Tasks;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Domain.Services.DiscountStrategies
{
    public class CustomerSegmentDiscountStrategy : IDiscountStrategy
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerSegmentDiscountStrategy(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public bool IsApplicable(Order order)
        {
            return !string.IsNullOrEmpty(order.CustomerSegment);
        }

        public async Task<decimal> CalculateDiscountAsync(Order order)
        {
            if (!IsApplicable(order))
                return 0;

            var customer = await _customerRepository.GetByIdAsync(order.CustomerId);
            if (customer == null)
                return 0;

            return customer.CustomerSegment.ToLower() switch
            {
                "premium" => 15,
                "regular" => 10,
                "basic" => 5,
                _ => 0
            };
        }
    }
} 