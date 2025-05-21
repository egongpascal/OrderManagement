using System;
using System.Threading.Tasks;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Domain.Services
{
    public class OrderAnalyticsService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderAnalyticsService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderAnalytics> GetAnalyticsAsync()
        {
            var averageOrderValue = await _orderRepository.GetAverageOrderValueAsync();
            var averageFulfillmentTime = await _orderRepository.GetAverageFulfillmentTimeAsync();

            return new OrderAnalytics
            {
                AverageOrderValue = averageOrderValue,
                AverageFulfillmentTime = averageFulfillmentTime
            };
        }
    }

    public class OrderAnalytics
    {
        public decimal AverageOrderValue { get; set; }
        public TimeSpan AverageFulfillmentTime { get; set; }
    }
} 