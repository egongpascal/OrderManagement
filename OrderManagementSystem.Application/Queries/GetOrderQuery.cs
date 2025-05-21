using System;
using MediatR;
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Queries
{
    public class GetOrderQuery : IRequest<OrderDto>
    {
        public Guid OrderId { get; set; }
    }
} 