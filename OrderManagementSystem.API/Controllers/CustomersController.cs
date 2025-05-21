using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Commands;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Application.Queries;

namespace OrderManagementSystem.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id)
        {
            var query = new GetCustomerQuery { CustomerId = id };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerDto>), 200)]
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
        {
            var query = new GetAllCustomersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCustomer), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }

        [HttpGet("{id}/analytics")]
        [ProducesResponseType(typeof(CustomerAnalyticsDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CustomerAnalyticsDto>> GetCustomerAnalytics(Guid id)
        {
            var query = new GetCustomerAnalyticsQuery { CustomerId = id };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }
    }
} 