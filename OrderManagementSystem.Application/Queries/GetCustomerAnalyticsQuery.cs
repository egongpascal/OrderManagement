using System;
using MediatR;
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Queries
{
    public class GetCustomerAnalyticsQuery : IRequest<CustomerAnalyticsDto>
    {
        public Guid CustomerId { get; set; }
    }
} 