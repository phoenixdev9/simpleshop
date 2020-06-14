using System.Collections.Generic;

namespace Domains.Entities
{
    public class Basket : Base
    {
        public ICollection<BasketItem> cart { get; set; }
        public decimal total { get; set; }
        public int totalItems { get; set; }
    }
}