using System;
using MediatR;
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Commands
{
    public class CreateCustomerCommand : IRequest<CustomerDto>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CustomerSegment { get; set; }
    }
} 