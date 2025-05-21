using System.Threading.Tasks;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Domain.Interfaces
{
    public interface IDiscountStrategy
    {
        Task<decimal> CalculateDiscountAsync(Order order);
        bool IsApplicable(Order order);
    }
} 