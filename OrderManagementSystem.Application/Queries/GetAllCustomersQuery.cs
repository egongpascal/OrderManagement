using System.Collections.Generic;
using MediatR;
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Queries
{
    public class GetAllCustomersQuery : IRequest<List<CustomerDto>>
    {
    }
} 