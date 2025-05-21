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
    /// Controller for managing orders in the system
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a specific order by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the order</param>
        /// <returns>The order details if found</returns>
        /// <response code="200">Returns the order</response>
        /// <response code="404">If the order is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
        {
            var query = new GetOrderQuery { OrderId = id };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Retrieves all orders for a specific customer
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer</param>
        /// <returns>A list of orders for the customer</returns>
        /// <response code="200">Returns the list of orders</response>
        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(typeof(List<OrderDto>), 200)]
        public async Task<ActionResult<List<OrderDto>>> GetCustomerOrders(Guid customerId)
        {
            var query = new GetCustomerOrdersQuery { CustomerId = customerId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="command">The order creation command containing order details</param>
        /// <returns>The newly created order</returns>
        /// <response code="201">Returns the newly created order</response>
        /// <response code="400">If the order data is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrder), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates the status of an existing order
        /// </summary>
        /// <param name="id">The unique identifier of the order</param>
        /// <param name="newStatus">The new status to set for the order</param>
        /// <returns>No content if successful</returns>
        /// <response code="200">If the status was updated successfully</response>
        /// <response code="400">If the status transition is invalid</response>
        /// <response code="404">If the order is not found</response>
        [HttpPut("{id}/status")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] string newStatus)
        {
            var command = new UpdateOrderStatusCommand
            {
                OrderId = id,
                NewStatus = newStatus
            };

            var result = await _mediator.Send(command);
            return result ? Ok() : BadRequest("Invalid status transition");
        }
    }
} 