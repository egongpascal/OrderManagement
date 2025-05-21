using System;
using MediatR;

namespace OrderManagementSystem.Application.Commands
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CustomerSegment { get; set; }
    }
} 