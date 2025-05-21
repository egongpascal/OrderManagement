using System;
using System.Threading.Tasks;
using Moq;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Services.DiscountStrategies;
using OrderManagementSystem.Domain.Interfaces;
using Xunit;

namespace OrderManagementSystemTest
{
    public class DiscountTests
    {
        public class OrderValueDiscountStrategyTests
        {
            private readonly OrderValueDiscountStrategy _strategy;

            public OrderValueDiscountStrategyTests()
            {
                _strategy = new OrderValueDiscountStrategy();
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(100, 0)]
            [InlineData(200, 10)]
            [InlineData(500, 15)]
            [InlineData(1000, 20)]
            public async Task CalculateDiscount_WithDifferentOrderValues_ReturnsCorrectDiscount(decimal orderValue, decimal expectedDiscount)
            {
                // Arrange
                var order = new Order(Guid.NewGuid(), "regular");
                typeof(Order).GetProperty("TotalAmount")?.SetValue(order, orderValue);

                // Act
                var discount = await _strategy.CalculateDiscountAsync(order);

                // Assert
                Assert.Equal(expectedDiscount, discount);
            }
        }

        public class CustomerSegmentDiscountStrategyTests
        {
            private readonly CustomerSegmentDiscountStrategy _strategy;
            private readonly Mock<ICustomerRepository> _mockCustomerRepository;

            public CustomerSegmentDiscountStrategyTests()
            {
                _mockCustomerRepository = new Mock<ICustomerRepository>();
                _strategy = new CustomerSegmentDiscountStrategy(_mockCustomerRepository.Object);
            }

            [Theory]
            [InlineData("premium", 15)]
            [InlineData("regular", 10)]
            [InlineData("basic", 5)]
            [InlineData("unknown", 0)]
            public async Task CalculateDiscount_WithDifferentCustomerSegments_ReturnsCorrectDiscount(string segment, decimal expectedDiscount)
            {
                // Arrange
                var customerId = Guid.NewGuid();
                var order = new Order(customerId, segment);
                var customer = new Customer("Pascal", "pascal@example.com", segment);
                _mockCustomerRepository.Setup(repo => repo.GetByIdAsync(customerId))
                    .ReturnsAsync(customer);

                // Act
                var discount = await _strategy.CalculateDiscountAsync(order);

                // Assert
                Assert.Equal(expectedDiscount, discount);
            }
        }
    }
} 