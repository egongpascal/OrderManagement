using System;
using System.Collections.Generic;
using MediatR;
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Queries
{
    public class GetCustomerOrdersQuery : IRequest<List<OrderDto>>
    {
        public Guid CustomerId { get; set; }
    }
} 