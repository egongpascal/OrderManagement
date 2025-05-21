using System;

namespace OrderManagementSystem.Application.DTOs
{
    public class CustomerAnalyticsDto
    {
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal AverageOrderValue { get; set; }
        public TimeSpan AverageFulfillmentTime { get; set; }
        public string MostOrderedProduct { get; set; }
        public int MostOrderedProductCount { get; set; }
    }
} 