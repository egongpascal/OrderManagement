using System.Linq;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Only seed if DB is empty
            if (!context.Customers.Any())
            {
                var customer1 = new Customer("Pascal", "pascal@example.com", "premium");
                var customer2 = new Customer("Egong", "egong@example.com", "regular");
                var customer3 = new Customer("TestCustomer", "testcustomer@example.com", "basic");
                context.Customers.AddRange(customer1, customer2, customer3);

                var order1 = new Order(customer1.Id, customer1.CustomerSegment);
                var order2 = new Order(customer2.Id, customer2.CustomerSegment);
                var order3 = new Order(customer3.Id, customer3.CustomerSegment);
                context.Orders.AddRange(order1, order2, order3);

                context.SaveChanges();
            }
        }
    }
} 