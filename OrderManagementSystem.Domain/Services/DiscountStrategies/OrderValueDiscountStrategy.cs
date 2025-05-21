using System.Threading.Tasks;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Domain.Services.DiscountStrategies
{
    public class OrderValueDiscountStrategy : IDiscountStrategy
    {
        public bool IsApplicable(Order order)
        {
            return order.TotalAmount > 0;
        }

        public Task<decimal> CalculateDiscountAsync(Order order)
        {
            if (!IsApplicable(order))
                return Task.FromResult(0m);

            return Task.FromResult(order.TotalAmount switch
            {
                var amount when amount >= 1000 => 20m,
                var amount when amount >= 500 => 15m,
                var amount when amount >= 200 => 10m,
                _ => 0m
            });
        }
    }
} 