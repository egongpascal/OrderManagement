using System;

namespace OrderManagementSystem.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice { get; private set; }

        private OrderItem() { } // For EF Core

        public OrderItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative", nameof(unitPrice));

            Id = Guid.NewGuid();
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            TotalPrice = unitPrice * quantity;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(newQuantity));

            Quantity = newQuantity;
            TotalPrice = UnitPrice * Quantity;
        }

        public void UpdateUnitPrice(decimal newUnitPrice)
        {
            if (newUnitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative", nameof(newUnitPrice));

            UnitPrice = newUnitPrice;
            TotalPrice = UnitPrice * Quantity;
        }
    }
} 