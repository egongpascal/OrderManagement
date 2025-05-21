using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Application.Commands.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.Id);
            if (customer == null)
                return false;

            customer.UpdateName(request.Name);
            customer.UpdateEmail(request.Email);
            customer.UpdateCustomerSegment(request.CustomerSegment);

            await _unitOfWork.Customers.UpdateAsync(customer);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
} 