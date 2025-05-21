using AutoMapper;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Customer, CustomerDto>();
        }
    }
} 