using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Application.Queries.Handlers
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
            return order != null ? _mapper.Map<OrderDto>(order) : null;
        }
    }
} 