using System;

namespace Domains.Entities
{
    public class OrderItem : Base
    {
        public OrderItem() { }

        public OrderItem(Guid productItemId, string productName, string pictureUrl, decimal price, int quantity)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
            Quantity = quantity;
        }
        public Guid ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}