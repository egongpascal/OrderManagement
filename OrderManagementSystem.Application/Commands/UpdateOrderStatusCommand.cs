using System;
using MediatR;

namespace OrderManagementSystem.Application.Commands
{
    public class UpdateOrderStatusCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public string NewStatus { get; set; }
    }
} 