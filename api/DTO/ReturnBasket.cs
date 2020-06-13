using System;

namespace api.DTO
{
    public class ReturnBasket
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
    }
}