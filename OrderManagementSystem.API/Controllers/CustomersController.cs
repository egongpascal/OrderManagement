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
    /// <summary>
    /// Controller for managing customers in the system
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a specific customer by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the customer</param>
        /// <returns>The customer details if found</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If the customer is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id)
        {
            var query = new GetCustomerQuery { CustomerId = id };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Retrieves all customers in the system
        /// </summary>
        /// <returns>A list of all customers</returns>
        /// <response code="200">Returns the list of customers</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerDto>), 200)]
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
        {
            var query = new GetAllCustomersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="command">The customer creation command containing customer details</param>
        /// <returns>The newly created customer</returns>
        /// <response code="201">Returns the newly created customer</response>
        /// <response code="400">If the customer data is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCustomer), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing customer's information
        /// </summary>
        /// <param name="id">The unique identifier of the customer</param>
        /// <param name="command">The customer update command containing new details</param>
        /// <returns>No content if successful</returns>
        /// <response code="200">If the customer was updated successfully</response>
        /// <response code="400">If the customer data is invalid</response>
        /// <response code="404">If the customer is not found</response>
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

        /// <summary>
        /// Retrieves analytics data for a specific customer
        /// </summary>
        /// <param name="id">The unique identifier of the customer</param>
        /// <returns>The customer's analytics data</returns>
        /// <response code="200">Returns the customer analytics</response>
        /// <response code="404">If the customer is not found</response>
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