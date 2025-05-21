using OrderManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagementSystem.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal DiscountedAmount { get; private set; }
        public string CustomerSegment { get; private set; }
        public ICollection<OrderItem> Items { get; private set; }

        private Order() { } // For EF Core

        public Order(Guid customerId, string customerSegment)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            CustomerSegment = customerSegment;
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatus.Created;
            Items = new List<OrderItem>();
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
            RecalculateTotal();
        }

        public void RemoveItem(Guid itemId)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                Items.Remove(item);
                RecalculateTotal();
            }
        }

        public void ApplyDiscount(decimal discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
                throw new ArgumentException("Discount percentage must be between 0 and 100");

            DiscountedAmount = TotalAmount * (1 - discountPercentage / 100);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            if (!IsValidStatusTransition(Status, newStatus))
                throw new InvalidOperationException($"Cannot transition from {Status} to {newStatus}");

            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }

        private void RecalculateTotal()
        {
            TotalAmount = Items.Sum(item => item.TotalPrice);
            DiscountedAmount = TotalAmount; // Reset discount when items change
            UpdatedAt = DateTime.UtcNow;
        }

        private bool IsValidStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {
            return (currentStatus, newStatus) switch
            {
                (OrderStatus.Created, OrderStatus.Processing) => true,
                (OrderStatus.Processing, OrderStatus.Shipped) => true,
                (OrderStatus.Shipped, OrderStatus.Delivered) => true,
                (OrderStatus.Created, OrderStatus.Cancelled) => true,
                (OrderStatus.Processing, OrderStatus.Cancelled) => true,
                _ => false
            };
        }
    }
} 