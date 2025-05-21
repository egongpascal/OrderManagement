using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Services;

namespace OrderManagementSystem.Application.Commands.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DiscountService _discountService;

        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            DiscountService discountService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _discountService = discountService;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(request.CustomerId, request.CustomerSegment);

            foreach (var itemDto in request.Items)
            {
                var item = new OrderItem(
                    itemDto.ProductId,
                    itemDto.ProductName,
                    itemDto.UnitPrice,
                    itemDto.Quantity);
                order.AddItem(item);
            }

            await _discountService.ApplyDiscountsAsync(order);
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<OrderDto>(order);
        }
    }
} 