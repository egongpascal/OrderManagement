# Order Management System

A .NET 8 Web API project that implements an order management system with advanced features including discounting, order tracking, and analytics.

## Features

### 1. Discounting System
- Implements multiple discount strategies based on:
  - Customer segments (e.g., Premium, Regular)
  - Order history and value
  - Custom promotion rules
- Flexible strategy pattern implementation for easy addition of new discount rules

### 2. Order Status Tracking
- Comprehensive order status management with state transitions
- Statuses include: Created, Processing, Shipped, Delivered, Cancelled
- Validation of status transitions to ensure business rules compliance

### 3. Analytics Endpoints
- Customer analytics with metrics like:
  - Average order value
  - Order fulfillment time
  - Order frequency
  - Total spending
- Order analytics with insights on:
  - Processing times
  - Status distribution
  - Value trends

## API Documentation

The API is documented using Swagger/OpenAPI annotations. You can access the Swagger UI at `/swagger` when running the application.

### Key Endpoints

#### Orders
- `GET /api/orders/{id}` - Get order details
- `GET /api/orders/customer/{customerId}` - Get customer's orders
- `POST /api/orders` - Create new order
- `PUT /api/orders/{id}/status` - Update order status

#### Customers
- `GET /api/customers/{id}` - Get customer details
- `GET /api/customers` - List all customers
- `POST /api/customers` - Create new customer
- `PUT /api/customers/{id}` - Update customer information
- `GET /api/customers/{id}/analytics` - Get customer analytics

## Testing

### Unit Tests
- Discount calculation logic tests
- Order status transition validation
- Customer analytics calculation

### Integration Tests
- API endpoint testing
- End-to-end order flow testing
- Customer analytics endpoint testing

## Performance Optimizations

1. **Caching Strategy**
   - Implemented caching for frequently accessed data
   - Customer analytics results caching
   - Order status caching

2. **Query Optimization**
   - Optimized database queries using proper indexing
   - Implemented efficient data loading patterns

## Technical Stack

- .NET 8
- Entity Framework Core
- MediatR for CQRS pattern
- FluentValidation for request validation
- AutoMapper for object mapping
- Swagger/OpenAPI for API documentation
- Serilog for logging

## Getting Started

1. Clone the repository
2. Restore NuGet packages
3. Update the database connection string in `appsettings.json`
4. Run the application
5. Access Swagger UI at `/swagger`

## Development Setup

```bash
# Restore dependencies
dotnet restore

# Run the application
dotnet run --project OrderManagementSystem.API

# Run tests
dotnet test
```

## Architecture

The solution follows Clean Architecture principles with the following layers:
- API Layer (Controllers, Middleware)
- Application Layer (Commands, Queries, DTOs)
- Domain Layer (Entities, Interfaces)
- Infrastructure Layer (Repositories, Services)

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request