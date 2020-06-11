using System;

namespace Domains.Entities
{
    public class BasketItem : Base
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}