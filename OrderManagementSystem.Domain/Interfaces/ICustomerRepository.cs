using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<IEnumerable<Customer>> GetBySegmentAsync(string segment);
        Task<Customer> AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Guid id);
        Task<int> GetOrderCountAsync(Guid customerId);
        Task<decimal> GetTotalSpentAsync(Guid customerId);
    }
} 