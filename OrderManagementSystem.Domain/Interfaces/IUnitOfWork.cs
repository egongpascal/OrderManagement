using System;
using System.Threading.Tasks;

namespace OrderManagementSystem.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }
        Task<int> CompleteAsync();
    }
} 