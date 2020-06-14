using System;
using System.Collections.Generic;
using Domains.Entities;

namespace api.DTO
{
    public class OrderReturnDTO
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public AddressDTO ShipToAddress { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public string Status { get; set; }
        public string BuyerEmail { get; set; }
        public decimal Total { get; set; }
    }
}