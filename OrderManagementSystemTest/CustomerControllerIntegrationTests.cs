using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using OrderManagementSystem.API;
using Xunit;
using System.Linq;
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystemTest
{

    public class CustomerControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CustomerControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateCustomer_WithValidData_ReturnsCreated()
        {
            // Arrange
            var customerData = new
            {
                Name = "IntegrationTestUser",
                Email = "integrationtestuser@example.com",
                CustomerSegment = "premium"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(customerData),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PostAsync("/api/customers", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("IntegrationTestUser", responseContent);
        }
    }
} 