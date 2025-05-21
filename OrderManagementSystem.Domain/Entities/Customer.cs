using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CustomerSegment { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public ICollection<Order> Orders { get; private set; }

        private Customer() { } // For EF Core

        public Customer(string name, string email, string customerSegment)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));
            if (string.IsNullOrWhiteSpace(customerSegment))
                throw new ArgumentException("Customer segment cannot be empty", nameof(customerSegment));

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            CustomerSegment = customerSegment;
            CreatedAt = DateTime.UtcNow;
            Orders = new List<Order>();
        }

        public void UpdateCustomerSegment(string newSegment)
        {
            if (string.IsNullOrWhiteSpace(newSegment))
                throw new ArgumentException("Customer segment cannot be empty", nameof(newSegment));

            CustomerSegment = newSegment;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email cannot be empty", nameof(newEmail));

            Email = newEmail;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty", nameof(newName));

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
} 