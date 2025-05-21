using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Infrastructure.Data;

namespace OrderManagementSystem.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _context.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.Orders)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetBySegmentAsync(string segment)
        {
            return await _context.Customers
                .Include(c => c.Orders)
                .Where(c => c.CustomerSegment == segment)
                .ToListAsync();
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            return customer;
        }

        public Task UpdateAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await GetByIdAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
        }

        public async Task<int> GetOrderCountAsync(Guid customerId)
        {
            return await _context.Orders
                .CountAsync(o => o.CustomerId == customerId);
        }

        public async Task<decimal> GetTotalSpentAsync(Guid customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId && o.Status == OrderStatus.Delivered)
                .SumAsync(o => o.TotalAmount);
        }
    }
} 