using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Application.Queries.Handlers
{
    public class GetCustomerAnalyticsQueryHandler : IRequestHandler<GetCustomerAnalyticsQuery, CustomerAnalyticsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerAnalyticsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerAnalyticsDto> Handle(GetCustomerAnalyticsQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
            if (customer == null)
                return null;

            var orders = await _unitOfWork.Orders.GetByCustomerIdAsync(request.CustomerId);
            var deliveredOrders = orders.Where(o => o.Status == Domain.Enums.OrderStatus.Delivered).ToList();

            if (!deliveredOrders.Any())
                return new CustomerAnalyticsDto();

            var totalSpent = deliveredOrders.Sum(o => o.TotalAmount);
            var averageOrderValue = totalSpent / deliveredOrders.Count;
            var averageFulfillmentTime = TimeSpan.FromTicks(
                deliveredOrders.Sum(o => (o.UpdatedAt ?? o.CreatedAt).Subtract(o.CreatedAt).Ticks) / deliveredOrders.Count);

            var mostOrderedProduct = deliveredOrders
                .SelectMany(o => o.Items)
                .GroupBy(i => i.ProductName)
                .OrderByDescending(g => g.Sum(i => i.Quantity))
                .FirstOrDefault();

            return new CustomerAnalyticsDto
            {
                TotalOrders = deliveredOrders.Count,
                TotalSpent = totalSpent,
                AverageOrderValue = averageOrderValue,
                AverageFulfillmentTime = averageFulfillmentTime,
                MostOrderedProduct = mostOrderedProduct?.Key,
                MostOrderedProductCount = mostOrderedProduct?.Sum(i => i.Quantity) ?? 0
            };
        }
    }
} 