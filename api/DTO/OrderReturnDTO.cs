using System;
using System.Collections.Generic;
using Domains.Entities;

namespace api.DTO
{
    public class OrderReturnDTO
    {
        public DateTime OrderDate { get; set; }
        public Address ShipToAddress { get; set; }
        public IReadOnlyList<Order> OrderItems { get; set; }
        public string Status { get; set; }
        public string BuyerEmail { get; set; }
    }
}