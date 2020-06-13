using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Order : Base
    {
        public Order() { }

        public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, Address shipToAddress)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            OrderItems = orderItems;
        }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public Address ShipToAddress { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string BuyerEmail { get; set; }
    }
}