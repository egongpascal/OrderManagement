using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Application.Commands.Handlers
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
            if (order == null)
                return false;

            if (!Enum.TryParse<OrderStatus>(request.NewStatus, out var newStatus))
                return false;

            try
            {
                order.UpdateStatus(newStatus);
                await _unitOfWork.Orders.UpdateAsync(order);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
} 