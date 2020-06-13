using System.Collections.Generic;

namespace api.DTO
{
    public class ReturnCheckout
    {
        public ReturnCheckout(IReadOnlyList<ReturnBasket> c, decimal t)
        {
            cart = c;
            total = t;
            totalItems = c.Count;
        }
        public IReadOnlyList<ReturnBasket> cart { get; set; }
        public decimal total { get; set; }
        public int totalItems { get; set; }
    }
}