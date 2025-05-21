using System;
using System.Collections.Generic;
using MediatR;
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Commands
{
    public class CreateOrderCommand : IRequest<OrderDto>
    {
        public Guid CustomerId { get; set; }
        public string CustomerSegment { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
} 